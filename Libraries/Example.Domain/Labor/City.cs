using Example.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Domain.Labor
{
    public class City : BaseEntity<int>
    {
        public string Name { get; set; }

        public ICollection<LaborOffice> LaborOffices { get; set; } = new HashSet<LaborOffice>();
    }
}
