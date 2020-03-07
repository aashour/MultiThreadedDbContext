using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Example.Core;
using Example.Core.Entity;

namespace Example.Data
{
    public class LaborRepository<TEntity, TId> : BaseRepository<TEntity, TId>
        where TEntity : BaseEntity<TId>
        where TId : struct
    {
        public LaborRepository(IAmbientDbContextLocator ambientDbContextLocator)
            : base(ambientDbContextLocator)
        {
        }

        internal override IDbContext DbContext
        {
            get
            {
                var dbContext = _ambientDbContextLocator.Get<LaborObjectContext>("LaborEntites");

                if (dbContext == null)
                    throw new InvalidOperationException("No ambient DbContext of type UserManagementDbContext found. This means that this repository method has been called outside of the scope of a DbContextScope. A repository must only be accessed within the scope of a DbContextScope, which takes care of creating the DbContext instances that the repositories need and making them available as ambient contexts. This is what ensures that, for any given DbContext-derived type, the same instance is used throughout the duration of a business transaction. To fix this issue, use IDbContextScopeFactory in your top-level business logic service method to create a DbContextScope that wraps the entire business transaction that your service method implements. Then access this repository within that scope. Refer to the comments in the IDbContextScope.cs file for more details.");

                return dbContext;
            }
        }
    }
}
