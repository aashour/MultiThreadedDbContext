using Example.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Data
{
    public abstract class BaseObjectContext : DbContext, IDbContext
    {
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        public BaseObjectContext(string nameOrConnectionString)
           : base(nameOrConnectionString)
        {
            Database.SetInitializer<BaseObjectContext>(null);
            //((IObjectContextAdapter)this).ObjectContext.SavingChanges += ObjectContext_SavingChanges;
            Database.Log = msg => Trace.WriteLine(msg);
        }

        private void ObjectContext_SavingChanges(object sender, EventArgs e)
        {
        }

        public void Detach<TEntity, TId>(TEntity entity)
            where TEntity : BaseEntity<TId>
            where TId : struct
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            ((IObjectContextAdapter)this).ObjectContext.Detach(entity);
        }

        public IEnumerable<TEntity> EntityFromSql<TEntity, TId>(string sql, params object[] parameters)
            where TEntity : BaseEntity<TId>
            where TId : struct
        {
            return this.Database.SqlQuery<TEntity>(CreateSqlWithParameters(sql, parameters), parameters);
        }

        public int ExecuteSqlCommand(string sql, bool doNotEnsureTransaction = false, int? timeout = null, params object[] parameters)
        {
            int? previousTimeout = null;
            if (timeout.HasValue)
            {
                //store previous timeout
                previousTimeout = ((IObjectContextAdapter)this).ObjectContext.CommandTimeout;
                ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = timeout;
            }

            var transactionalBehavior = doNotEnsureTransaction
                ? TransactionalBehavior.DoNotEnsureTransaction
                : TransactionalBehavior.EnsureTransaction;
            var result = this.Database.ExecuteSqlCommand(transactionalBehavior, sql, parameters);

            if (timeout.HasValue)
            {
                //Set previous timeout back
                ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = previousTimeout;
            }
            return result;
        }

        public string GenerateCreateScript()
        {
            return ((IObjectContextAdapter)this).ObjectContext.CreateDatabaseScript();
        }

        public void Reload<TEntity, TId>(TEntity entity)
            where TEntity : BaseEntity<TId>
            where TId : struct
        {
            this.Entry(entity).Reload();
        }

        public DbSet<TEntity> Set<TEntity, TId>()
            where TEntity : BaseEntity<TId>
            where TId : struct
        {
            return base.Set<TEntity>();
        }

        protected virtual string CreateSqlWithParameters(string sql, params object[] parameters)
        {
            //add parameters to sql
            for (var i = 0; i <= (parameters?.Length ?? 0) - 1; i++)
            {
                if (!(parameters[i] is DbParameter parameter))
                    continue;

                sql = $"{sql}{(i > 0 ? "," : string.Empty)} @{parameter.ParameterName}";

                //whether parameter is output
                if (parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Output)
                    sql = $"{sql} output";
            }

            return sql;
        }

        internal EntityKeyMember GetPrimaryKey(DbEntityEntry entity)
        {
            var objectStateEntry = ((IObjectContextAdapter)this).ObjectContext.ObjectStateManager.GetObjectStateEntry(entity.Entity);

            if (objectStateEntry.EntityKey == null)
            {
                return null;
            }

            if (entity.State == EntityState.Modified || entity.State == EntityState.Deleted)
            {
                if (objectStateEntry.EntityKey.EntityKeyValues != null && objectStateEntry.EntityKey.EntityKeyValues.Length == 1)
                {
                    return objectStateEntry.EntityKey.EntityKeyValues[0];
                }
                else
                {
                    return null;
                }
            }
            else if (entity.State == EntityState.Added && objectStateEntry.EntitySet.ElementType.KeyMembers.Count == 1)
            {
                foreach (string propertyName in entity.CurrentValues.PropertyNames) // Usually ID is the first property, but cannot depend on that.
                {
                    var property = entity.Property(propertyName);
                    if (property == null)
                    {
                        continue;
                    }
                    string keyName = objectStateEntry.EntitySet.ElementType.KeyMembers[0].Name;
                    string currentValue = property.CurrentValue == null ? null : property.CurrentValue.ToString(); //Convert.ToString(property.CurrentValue); convert causes issue

                    if (propertyName == keyName)
                    {
                        if (long.TryParse(currentValue, out long objIdValue))
                        {
                            return new EntityKeyMember(keyName, objIdValue);
                        }
                        else
                        {
                            break;
                        }
                    }

                }
            }

            return null;
        }

        internal string GetTableName(Type type)
        {
            var metadata = ((IObjectContextAdapter)this).ObjectContext.MetadataWorkspace;

            // Get the part of the model that contains info about the actual CLR types
            var objectItemCollection = ((ObjectItemCollection)metadata.GetItemCollection(DataSpace.OSpace));

            // Get the entity type from the model that maps to the CLR type
            var entityType = metadata
                    .GetItems<EntityType>(DataSpace.OSpace)
                    .Single(e => objectItemCollection.GetClrType(e) == type);

            // Get the entity set that uses this entity type
            var entitySet = metadata
                .GetItems<EntityContainer>(DataSpace.CSpace)
                .Single()
                .EntitySets
                .Single(s => s.ElementType.Name == entityType.Name);

            // Find the mapping between conceptual and storage model for this entity set
            var mapping = metadata.GetItems<EntityContainerMapping>(DataSpace.CSSpace)
                    .Single()
                    .EntitySetMappings
                    .Single(s => s.EntitySet == entitySet);

            // Find the storage entity set (table) that the entity is mapped
            var table = mapping
                .EntityTypeMappings.Single()
                .Fragments.Single()
                .StoreEntitySet;

            // Return the table name from the storage entity set
            return (string)table.Table;
        }
    }
}
