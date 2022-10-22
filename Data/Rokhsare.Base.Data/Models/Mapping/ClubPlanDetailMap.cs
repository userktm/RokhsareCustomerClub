using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Rokhsare.Models.Mapping
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
            this.Property(t => t.PercentOFGiftCredit).HasColumnName("PercentOFGiftCredit");
            this.Property(t => t.LimitUseCreditResort).HasColumnName("LimitUseCreditResort");
            this.Property(t => t.LimitUseCreditForce).HasColumnName("LimitUseCreditForce");
            this.Property(t => t.LimitUserCreditPercent).HasColumnName("LimitUserCreditPercent");
        }
    }
}
