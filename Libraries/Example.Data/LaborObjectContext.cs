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
    public class LaborObjectContext : BaseObjectContext
    {
        public LaborObjectContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var typeFinder = new AppDomainTypeFinder();
            //dynamically load all entity and query type configurations
            var typeConfigurations = typeFinder.FindClassesOfType(typeof(LaborEntityTypeConfiguration<,>), true);
            foreach (var typeConfiguration in typeConfigurations)
            {
                var mappingConfiguration = (IMappingConfiguration)Activator.CreateInstance(typeConfiguration);
                mappingConfiguration.ApplyConfiguration(modelBuilder);
            }

            //modelBuilder.Configurations.AddFromAssembly(Assembly.GetAssembly(typeof(NitaqatEntityTypeConfiguration<,>)));

            base.OnModelCreating(modelBuilder);
        }
    }
}
