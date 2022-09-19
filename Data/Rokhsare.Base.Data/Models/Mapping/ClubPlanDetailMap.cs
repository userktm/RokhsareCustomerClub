using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Rokhsare.Base.Data.Models.Mapping
{
    public class ClubPlanDetailMap : EntityTypeConfiguration<ClubPlanDetail>
    {
        public ClubPlanDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.ClubPlanDetailId);

            // Properties
            // Table & Column Mappings
            this.ToTable("ClubPlanDetails");
            this.Property(t => t.ClubPlanDetailId).HasColumnName("ClubPlanDetailId");
        }
    }
}
