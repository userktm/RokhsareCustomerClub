using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Rokhsare.Base.Data.Models.Mapping
{
    public class ClubPlanGroupMap : EntityTypeConfiguration<ClubPlanGroup>
    {
        public ClubPlanGroupMap()
        {
            // Primary Key
            this.HasKey(t => t.ClubPlanGroupId);

            // Properties
            this.Property(t => t.ClubPlanGroupId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ClubPlanGroups");
            this.Property(t => t.ClubPlanGroupId).HasColumnName("ClubPlanGroupId");
            this.Property(t => t.ClubPlanGroupNumber).HasColumnName("ClubPlanGroupNumber");
            this.Property(t => t.ClubPlanId).HasColumnName("ClubPlanId");

            // Relationships
            this.HasRequired(t => t.ClubPlan)
                .WithMany(t => t.ClubPlanGroups)
                .HasForeignKey(d => d.ClubPlanId);

        }
    }
}
