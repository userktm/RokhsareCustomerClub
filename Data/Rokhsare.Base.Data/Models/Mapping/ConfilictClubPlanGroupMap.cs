using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Rokhsare.Models.Mapping
{
    public class ConfilictClubPlanGroupMap : EntityTypeConfiguration<ConfilictClubPlanGroup>
    {
        public ConfilictClubPlanGroupMap()
        {
            // Primary Key
            this.HasKey(t => t.ConfilictClubPlanGroupId);

            // Properties
            this.Property(t => t.ConfilictClubPlanGroupId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ConfilictClubPlanGroups");
            this.Property(t => t.ConfilictClubPlanGroupId).HasColumnName("ConfilictClubPlanGroupId");
            this.Property(t => t.ConfilictClubPlanGroupNumber).HasColumnName("ConfilictClubPlanGroupNumber");
            this.Property(t => t.ClubPlanId).HasColumnName("ClubPlanId");

            // Relationships
            this.HasRequired(t => t.ClubPlan)
                .WithMany(t => t.ConfilictClubPlanGroups)
                .HasForeignKey(d => d.ClubPlanId);

        }
    }
}
