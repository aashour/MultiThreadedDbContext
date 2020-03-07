using Example.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Domain.Labor
{
    public class LaborOffice : BaseEntity<int>
    {
        public int? CityId { get; set; }
        public string Name { get; set; }
        public virtual City City { get; set; }        
    }
}
