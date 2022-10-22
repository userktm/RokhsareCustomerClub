using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Rokhsare.Models.Mapping
{
    public class BusinesUnitClubPlanMap : EntityTypeConfiguration<BusinesUnitClubPlan>
    {
        public BusinesUnitClubPlanMap()
        {
            // Primary Key
            this.HasKey(t => t.BusinesUniClubPlanId);

            // Properties
            // Table & Column Mappings
            this.ToTable("BusinesUnitClubPlans");
            this.Property(t => t.BusinesUniClubPlanId).HasColumnName("BusinesUniClubPlanId");
            this.Property(t => t.BusinesUnitId).HasColumnName("BusinesUnitId");
            this.Property(t => t.ClubPlanId).HasColumnName("ClubPlanId");

            // Relationships
            this.HasRequired(t => t.BusinessUnit)
                .WithMany(t => t.BusinesUnitClubPlans)
                .HasForeignKey(d => d.BusinesUnitId);
            this.HasRequired(t => t.ClubPlan)
                .WithMany(t => t.BusinesUnitClubPlans)
                .HasForeignKey(d => d.ClubPlanId);

        }
    }
}
