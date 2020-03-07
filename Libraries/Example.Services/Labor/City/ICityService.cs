using Example.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Services.Labor.City
{
    public interface ICityService
    {
        Example.Domain.Labor.City GetCityById(int cityId);
        void DeleteCity(Example.Domain.Labor.City city);

        IPagedList<Example.Domain.Labor.City> GetCities(string cityName = null,
            string laborOfficeId = null,
            bool loadOnlyWithLaborOffices = false,
            int pageIndex = 0, 
            int pageSize = int.MaxValue,
            bool showHidden = false);

        void InsertCity(Example.Domain.Labor.City city);

        void UpdateCity(Example.Domain.Labor.City city);
    }
}
