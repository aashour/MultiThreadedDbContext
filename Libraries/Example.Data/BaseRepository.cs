using Example.Core;
using Example.Core.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Data
{
    public abstract class BaseRepository<TEntity, TId> : IRepository<TEntity, TId>
        where TEntity : BaseEntity<TId>
        where TId : struct
    {
        internal readonly IAmbientDbContextLocator _ambientDbContextLocator;
        public BaseRepository(IAmbientDbContextLocator ambientDbContextLocator)
        {
            if (ambientDbContextLocator == null) throw new ArgumentNullException("ambientDbContextLocator");
            _ambientDbContextLocator = ambientDbContextLocator;
        }

        internal abstract IDbContext DbContext { get; }
        protected virtual DbSet<TEntity> Entities
        {
            get
            {
                return DbContext.Set<TEntity, TId>();
            }
        }
        public IQueryable<TEntity> Table => Entities;

        public IQueryable<TEntity> TableNoTracking => Entities.AsNoTracking();

        public void Delete(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            Entities.Remove(entity);
        }

        public void Delete(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));
            Entities.RemoveRange(entities);
        }

        public TEntity GetById(TId id)
        {
            return Entities.Find(id);
        }

        public void Add(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            Entities.Add(entity);
        }

        public void Add(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));
            Entities.AddRange(entities);
        }

        public void Reload(TEntity entity)
        {
            DbContext.Reload<TEntity, TId>(entity);
        }
    }
}
