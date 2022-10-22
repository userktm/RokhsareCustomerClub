using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Rokhsare.Models.Mapping
{
    public class UserPlanMap : EntityTypeConfiguration<UserPlan>
    {
        public UserPlanMap()
        {
            // Primary Key
            this.HasKey(t => t.UserPlanId);

            // Properties
            // Table & Column Mappings
            this.ToTable("UserPlans");
            this.Property(t => t.UserPlanId).HasColumnName("UserPlanId");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.ClubPlanId).HasColumnName("ClubPlanId");
            this.Property(t => t.ExpireDate).HasColumnName("ExpireDate");

            // Relationships
            this.HasRequired(t => t.ClubPlan)
                .WithMany(t => t.UserPlans)
                .HasForeignKey(d => d.ClubPlanId);
            this.HasRequired(t => t.User)
                .WithMany(t => t.UserPlans)
                .HasForeignKey(d => d.UserId);

        }
    }
}
