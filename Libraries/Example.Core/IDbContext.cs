using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

namespace Example.Core
{
    public partial interface IDbContext : IDisposable
    {
        DbSet<TEntity> Set<TEntity, TId>()
            where TEntity : BaseEntity<TId>
            where TId : struct;

        int SaveChanges();

        string GenerateCreateScript();

        //IQueryable<TQuery> QueryFromSql<TQuery>(string sql, params object[] parameters) where TQuery : class;
        IEnumerable<TEntity> EntityFromSql<TEntity, TId>(string sql, params object[] parameters)
            where TEntity : BaseEntity<TId>
            where TId : struct;

        int ExecuteSqlCommand(string sql, bool doNotEnsureTransaction = false, int? timeout = null, params object[] parameters);
        void Detach<TEntity, TId>(TEntity entity)
            where TEntity : BaseEntity<TId>
            where TId : struct;

        void Reload<TEntity, TId>(TEntity entity)
            where TEntity : BaseEntity<TId>
            where TId : struct;

        Database Database { get; }

        DbContextConfiguration Configuration { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        Task<int> SaveChangesAsync();
    }
}
