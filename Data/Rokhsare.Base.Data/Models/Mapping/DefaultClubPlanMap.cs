using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Rokhsare.Models.Mapping
{
    public class DefaultClubPlanMap : EntityTypeConfiguration<DefaultClubPlan>
    {
        public DefaultClubPlanMap()
        {
            // Primary Key
            this.HasKey(t => t.DefaulteClubPlanId);

            // Properties
            // Table & Column Mappings
            this.ToTable("DefaultClubPlans");
            this.Property(t => t.DefaulteClubPlanId).HasColumnName("DefaulteClubPlanId");
            this.Property(t => t.BusinessUnitId).HasColumnName("BusinessUnitId");
            this.Property(t => t.ClubPlanId).HasColumnName("ClubPlanId");

            // Relationships
            this.HasRequired(t => t.BusinessUnit)
                .WithMany(t => t.DefaultClubPlans)
                .HasForeignKey(d => d.BusinessUnitId);
            this.HasRequired(t => t.ClubPlan)
                .WithMany(t => t.DefaultClubPlans)
                .HasForeignKey(d => d.ClubPlanId);

        }
    }
}
