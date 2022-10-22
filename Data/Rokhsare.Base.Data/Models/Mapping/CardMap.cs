using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Rokhsare.Models.Mapping
{
    public class CardMap : EntityTypeConfiguration<Card>
    {
        public CardMap()
        {
            // Primary Key
            this.HasKey(t => t.CardId);

            // Properties
            this.Property(t => t.CardNumber)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Cards");
            this.Property(t => t.CardId).HasColumnName("CardId");
            this.Property(t => t.ClubPlanId).HasColumnName("ClubPlanId");
            this.Property(t => t.CardNumber).HasColumnName("CardNumber");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.Creator).HasColumnName("Creator");
            this.Property(t => t.ExpireDate).HasColumnName("ExpireDate");

            // Relationships
            this.HasOptional(t => t.ClubPlan)
                .WithMany(t => t.Cards)
                .HasForeignKey(d => d.ClubPlanId);
            this.HasRequired(t => t.User)
                .WithMany(t => t.Cards)
                .HasForeignKey(d => d.Creator);

        }
    }
}
