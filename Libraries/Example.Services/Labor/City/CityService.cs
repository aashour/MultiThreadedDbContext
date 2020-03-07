using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Example.Core;
using Example.Core.Entity;
using Example.Data;
using Example.Domain.Labor;

namespace Example.Services.Labor.City
{
    public class CityService : ICityService
    {
        private readonly IDbContextScopeFactory _dbContextScopeFactory;
        private readonly IRepository<Domain.Labor.City, int> _cityRepository;

        public CityService(IDbContextScopeFactory dbContextScopeFactory, IRepository<Domain.Labor.City, int> cityRepository)
        {
            if (dbContextScopeFactory == null) throw new ArgumentNullException("dbContextScopeFactory");
            if (cityRepository == null) throw new ArgumentNullException("cityRepository");
            _dbContextScopeFactory = dbContextScopeFactory;
            _cityRepository = cityRepository;
        }

        public void DeleteCity(Domain.Labor.City city)
        {
            using (var dbContextScope = _dbContextScopeFactory.Create())
            {
                _cityRepository.Delete(city);
                dbContextScope.SaveChanges();
            }
        }

        public IPagedList<Domain.Labor.City> GetCities(string cityName = null, string laborOfficeId = null, bool loadOnlyWithLaborOffices = false, int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false)
        {
            throw new NotImplementedException();
        }

        public Domain.Labor.City GetCityById(int cityId)
        {
            using (var dbContextScope = _dbContextScopeFactory.CreateReadOnly())
            {
                Domain.Labor.City city = _cityRepository.GetById(cityId);

                if (city == null)
                    throw new ArgumentException($"Invalid value provided for {nameof(cityId)}: [{cityId}]. Couldn't find a user with this ID.");

                return city;
            }
        }

        public void InsertCity(Domain.Labor.City city)
        {
            using (var dbContextScope = _dbContextScopeFactory.Create())
            {
                _cityRepository.Add(city);
                dbContextScope.SaveChanges();
            }
        }

        public void UpdateCity(Domain.Labor.City city)
        {
            using (var dbContextScope = _dbContextScopeFactory.Create())
            {
                var dbContext = dbContextScope.DbContexts.Get<LaborObjectContext>("LaborEntites");
                Domain.Labor.City cityToUpdate = _cityRepository.GetById(city.Id);
                if (cityToUpdate == null)
                    throw new ArgumentException($"Invalid {nameof(cityToUpdate)} provided: {0}. Couldn't find a User with this ID.");

                // Simulate the calculation of a credit score taking some time
                cityToUpdate.Name = city.Name;
                dbContextScope.SaveChanges();
            }
        }
    }
}
