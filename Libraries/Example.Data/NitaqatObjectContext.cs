using Example.Core.Infrastructure;
using Example.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Example.Data
{
    public class NitaqatObjectContext : BaseObjectContext
    {
        public NitaqatObjectContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var typeFinder = new AppDomainTypeFinder();
            //dynamically load all entity and query type configurations
            var typeConfigurations = typeFinder.FindClassesOfType(typeof(NitaqatEntityTypeConfiguration<,>), true);
            foreach (var typeConfiguration in typeConfigurations)
            {
                var mappingConfiguration = (IMappingConfiguration)Activator.CreateInstance(typeConfiguration);
                mappingConfiguration.ApplyConfiguration(modelBuilder);
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}
