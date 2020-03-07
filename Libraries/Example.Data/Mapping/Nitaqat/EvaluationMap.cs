using Example.Domain.Nitaqat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Data.Mapping.Nitaqat
{
    public class EvaluationMap : NitaqatEntityTypeConfiguration<Evaluation, int>
    {
        public override void Configure()
        {
            ToTable("Evaluation");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.SizeId).HasColumnName("Size_Id");
            Property(t => t.ColorId).HasColumnName("Color_Id");
            HasRequired(t => t.Color).WithMany(t => t.Evaluations).HasForeignKey(d => d.ColorId);
        }
    }
}
