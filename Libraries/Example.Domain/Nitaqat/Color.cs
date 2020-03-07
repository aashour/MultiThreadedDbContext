using Example.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Domain.Nitaqat
{
    public class Color : BaseEntity<int>
    {
        public string Name { get; set; }
        public string NameEnglish { get; set; }
        public virtual ICollection<Evaluation> Evaluations { get; set; } = new List<Evaluation>();
    }
}
