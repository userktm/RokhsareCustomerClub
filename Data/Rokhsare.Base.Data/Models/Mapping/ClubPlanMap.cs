using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Rokhsare.Models.Mapping
{
    public class ClubPlanMap : EntityTypeConfiguration<ClubPlan>
    {
        public ClubPlanMap()
        {
            // Primary Key
            this.HasKey(t => t.ClubPlanId);

            // Properties
            this.Property(t => t.ClubPlanName)
                .IsRequired()
                .HasMaxLength(500);

            this.Property(t => t.ClubPlanDescription)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("ClubPlans");
            this.Property(t => t.ClubPlanId).HasColumnName("ClubPlanId");
            this.Property(t => t.ClubPlanDetailId).HasColumnName("ClubPlanDetailId");
            this.Property(t => t.ClubPlanName).HasColumnName("ClubPlanName");
            this.Property(t => t.ClubPlanDescription).HasColumnName("ClubPlanDescription");
            this.Property(t => t.CreditEnhanceSMSTemplate).HasColumnName("CreditEnhanceSMSTemplate");
            this.Property(t => t.CreditReduceSMSTemplate).HasColumnName("CreditReduceSMSTemplate");

            // Relationships
            this.HasRequired(t => t.ClubPlanDetail)
                .WithMany(t => t.ClubPlans)
                .HasForeignKey(d => d.ClubPlanDetailId);

        }
    }
}
