using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Example.Core;
using Example.Core.Entity;
using Example.Domain.Nitaqat;

namespace Example.Services.Nitaqat.Color
{
    public class ColorService : IColorService
    {
        private readonly IDbContextScopeFactory _dbContextScopeFactory;
        private readonly IRepository<Domain.Nitaqat.Color, int> _colorRepository;

        public ColorService(IDbContextScopeFactory dbContextScopeFactory, IRepository<Domain.Nitaqat.Color, int> colorRepository)
        {
            if (dbContextScopeFactory == null) throw new ArgumentNullException("dbContextScopeFactory");
            if (colorRepository == null) throw new ArgumentNullException("colorRepository");
            _dbContextScopeFactory = dbContextScopeFactory;
            _colorRepository = colorRepository;
        }
        public void DeleteColor(Domain.Nitaqat.Color Color)
        {
            throw new NotImplementedException();
        }

        public IPagedList<Domain.Nitaqat.Color> GetCities(string ColorName = null, string laborOfficeId = null, bool loadOnlyWithLaborOffices = false, int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false)
        {
            throw new NotImplementedException();
        }

        public Domain.Nitaqat.Color GetColorById(int colorId)
        {
            using (var dbContextScope = _dbContextScopeFactory.CreateReadOnly())
            {
                Domain.Nitaqat.Color color = _colorRepository.GetById(colorId);

                if (color == null)
                    throw new ArgumentException($"Invalid value provided for {nameof(colorId)}: [{colorId}]. Couldn't find a user with this ID.");

                return color;
            }
        }

        public void InsertColor(Domain.Nitaqat.Color color)
        {
            using (var dbContextScope = _dbContextScopeFactory.Create())
            {
                _colorRepository.Add(color);
                dbContextScope.SaveChanges();
            }
        }

        public void UpdateColor(Domain.Nitaqat.Color color)
        {
            using (var dbContextScope = _dbContextScopeFactory.Create())
            {
                Domain.Nitaqat.Color colorToUpdate = _colorRepository.GetById(color.Id);
                if (colorToUpdate == null)
                    throw new ArgumentException($"Invalid {nameof(colorToUpdate)} provided: {0}. Couldn't find a User with this ID.");

                // Simulate the calculation of a credit score taking some time
                colorToUpdate.Name = color.Name;
                dbContextScope.SaveChanges();
            }
        }
    }
}
