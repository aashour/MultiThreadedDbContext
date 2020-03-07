using Example.Domain.Labor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Data.Mapping.Labor
{
    public class CityMap : LaborEntityTypeConfiguration<City, int>
    {
        public override void Configure()
        {
            this.Property(t => t.Name).IsRequired().HasMaxLength(100);
            this.ToTable("Lookup_City");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");

        }
    }
}
