using Example.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Services.Nitaqat.Color
{
    public interface IColorService
    {
        Example.Domain.Nitaqat.Color GetColorById(int ColorId);
        void DeleteColor(Example.Domain.Nitaqat.Color color);

        IPagedList<Example.Domain.Nitaqat.Color> GetCities(string colorName = null,
            string laborOfficeId = null,
            bool loadOnlyWithLaborOffices = false,
            int pageIndex = 0,
            int pageSize = int.MaxValue,
            bool showHidden = false);

        void InsertColor(Example.Domain.Nitaqat.Color color);

        void UpdateColor(Example.Domain.Nitaqat.Color color);
    }
}
