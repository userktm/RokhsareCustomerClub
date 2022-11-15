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
            this.Property(t => t.BusinessUnitID).HasColumnName("BusinessUnitID");
            this.Property(t => t.PercentOFGiftCredit).HasColumnName("PercentOFGiftCredit");
            this.Property(t => t.FromCustomerPaymentPrice).HasColumnName("FromCustomerPaymentPrice");
            this.Property(t => t.ToCustomerPaymentPrice).HasColumnName("ToCustomerPaymentPrice");
            this.Property(t => t.FromCustomerSumPaymentPrice).HasColumnName("FromCustomerSumPaymentPrice");
            this.Property(t => t.ToCustomerSumPaymentPrice).HasColumnName("ToCustomerSumPaymentPrice");

            // Relationships
            this.HasRequired(t => t.BusinessUnits)
                .WithMany(t => t.ClubPlanDetails)
                .HasForeignKey(d => d.BusinessUnitID);
        }
    }
}
