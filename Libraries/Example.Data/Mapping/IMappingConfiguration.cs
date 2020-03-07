using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Data.Mapping
{
    public interface IMappingConfiguration
    {
        void ApplyConfiguration(DbModelBuilder modelBuilder);
    }
}
