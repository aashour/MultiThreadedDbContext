using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Core
{
    public partial interface IRepository<TEntity, TId> where TEntity : BaseEntity<TId> where TId : struct
    {
        TEntity GetById(TId id);
        void Add(TEntity entity);
        void Add(IEnumerable<TEntity> entities);
        //void Update(TEntity entity);
        //void Update(IEnumerable<TEntity> entities);
        void Delete(TEntity entity);
        void Delete(IEnumerable<TEntity> entities);
        void Reload(TEntity entity);
        IQueryable<TEntity> Table { get; }
        IQueryable<TEntity> TableNoTracking { get; }
    }
}
