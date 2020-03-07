using Example.Domain.Nitaqat;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Data.Mapping.Nitaqat
{
    public class ColorMap : NitaqatEntityTypeConfiguration<Color, int>
    {
        public override void Configure()
        {
            Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(t => t.Name).IsRequired().HasMaxLength(20);
            Property(t => t.NameEnglish).IsRequired().HasMaxLength(20);
            ToTable("Color");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.Name).HasColumnName("Name");
            Property(t => t.NameEnglish).HasColumnName("Name_English");
        }
    }
}
