using Example.Domain.Labor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Data.Mapping.Labor
{
    public class LaborOfficeMap : LaborEntityTypeConfiguration<LaborOffice, int>
    {
        public override void Configure()
        {
            this.Property(t => t.Name).HasMaxLength(40);
            this.ToTable("MOL_LaborOffice");
            this.Property(t => t.Id).HasColumnName("PK_LaborOfficeId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.HasOptional(t => t.City).WithMany(t => t.LaborOffices).HasForeignKey(d => d.CityId);
        }
    }
}
