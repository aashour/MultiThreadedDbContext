using Autofac;
using Example.Core.Infrastructure;
using Example.Core.Infrastructure.DependencyManagement;
using Example.Data;
using Example.Data.Entity;
using Example.Domain.Labor;
using Example.Services.Labor.City;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Example.ConsoleApp.Framework;
using Example.ConsoleApp.Framework.Extensions;
using Example.Services.Nitaqat.Color;
using Example.Domain.Nitaqat;

namespace Example.ConsoleApp
{
    class Program
    {
        private static IContainer Container { get; set; }
        static void Main(string[] args)
        {

            object t = 3;
            int i = 3.ChangeType<int>();

            var containerBuilder = new ContainerBuilder();

            var typeFinder = new AppDomainTypeFinder();
            containerBuilder.RegisterInstance(typeFinder).As<ITypeFinder>().SingleInstance();

            //find dependency registrars provided by other assemblies
            var dependencyRegistrars = typeFinder.FindClassesOfType<IDependencyRegistrar>();

            //create and sort instances of dependency registrars
            var instances = dependencyRegistrars
                .Select(dependencyRegistrar => (IDependencyRegistrar)Activator.CreateInstance(dependencyRegistrar))
                .OrderBy(dependencyRegistrar => dependencyRegistrar.Order);

            //register all provided dependencies
            foreach (var dependencyRegistrar in instances)
                dependencyRegistrar.Register(containerBuilder);
            Container = containerBuilder.Build();

            using (var scope = Container.BeginLifetimeScope())
            {
                var cityService = scope.Resolve<ICityService>();
                List<City> cities = new List<City> { new City { Name = "My City2" }, new City { Name = "My City3" }, new City { Name = "My City4" }, new City { Name = "My City5" } };

                Parallel.ForEach(cities, city => cityService.InsertCity(city));

                var cityToUpdate = cities[cities.Count - 1];
                cityToUpdate.Name = "update";
                cityService.UpdateCity(cityToUpdate);


                var colorService = scope.Resolve<IColorService>();

                List<Color> colors = new List<Color> { new Color { Name = "My City2" }, new Color { Name = "My City3" }, new Color { Name = "My City4" }, new Color { Name = "My City5" } };

                Parallel.ForEach(colors, color => colorService.InsertColor(color));

                var colorToUpdate = cities[cities.Count - 1];
                colorToUpdate.Name = "update";
                cityService.UpdateCity(colorToUpdate);
            }
        }
    }
}
