using Example.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Data.Mapping
{
    public abstract class LaborEntityTypeConfiguration<TEntity, TId> : EntityTypeConfiguration<TEntity>, IMappingConfiguration
        where TEntity : BaseEntity<TId>
        where TId : struct
    {

        public abstract void Configure();

        public void ApplyConfiguration(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TEntity>().HasKey(t => t.Id);
            Configure();
        }
    }
}