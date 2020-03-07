using Example.Core;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace Example.Data.Mapping
{
    public abstract class NitaqatEntityTypeConfiguration<TEntity, TId> : EntityTypeConfiguration<TEntity>, IMappingConfiguration
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