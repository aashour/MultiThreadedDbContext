using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Core.Entity
{
    /// <summary>
    /// Convenience methods to retrieve ambient DbContext instances. 
    /// </summary>
    public interface IAmbientDbContextLocator
    {
        /// <summary>
        /// If called within the scope of a DbContextScope, gets or creates 
        /// the ambient DbContext instance for the provided DbContext type. 
        /// 
        /// Otherwise returns null. 
        /// </summary>
        TDbContext Get<TDbContext>(string nameOrConnectionString) where TDbContext : class, IDbContext;
    }
}
