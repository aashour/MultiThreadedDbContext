using Example.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Domain.Nitaqat
{
    public partial class Evaluation : BaseEntity<int>
    {
        public int SizeId { get; set; }
        public int ColorId { get; set; }
        public virtual Color Color { get; set; }
    }
}
