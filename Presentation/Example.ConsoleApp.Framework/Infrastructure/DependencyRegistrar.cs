using Autofac;
using Example.Core;
using Example.Core.Entity;
using Example.Core.Infrastructure.DependencyManagement;
using Example.Data;
using Example.Data.Entity;
using Example.Services.Labor.City;
using Example.Services.Nitaqat.Color;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.ConsoleApp.Framework.Infrastructure
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public int Order => 0;

        public void Register(ContainerBuilder builder)
        {
            builder.RegisterType<DbContextScopeFactory>().As<IDbContextScopeFactory>().InstancePerDependency();
            builder.RegisterType<AmbientDbContextLocator>().As<IAmbientDbContextLocator>().InstancePerDependency();
            //repositories
            builder.RegisterGeneric(typeof(LaborRepository<,>)).As(typeof(IRepository<,>)).InstancePerDependency();
            builder.RegisterGeneric(typeof(NitaqatRepository<,>)).As(typeof(IRepository<,>)).InstancePerDependency();

            builder.RegisterType<CityService>().As<ICityService>().InstancePerDependency();
            builder.RegisterType<ColorService>().As<IColorService>().InstancePerDependency();

        }
    }
}
