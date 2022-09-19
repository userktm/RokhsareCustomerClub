using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Rokhsare.Base.Data.Models.Mapping
{
    public class ClubFactureMap : EntityTypeConfiguration<ClubFacture>
    {
        public ClubFactureMap()
        {
            // Primary Key
            this.HasKey(t => t.ClubFactureId);

            // Properties
            this.Property(t => t.ProductName)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("ClubFactures");
            this.Property(t => t.ClubFactureId).HasColumnName("ClubFactureId");
            this.Property(t => t.BusinessUnitId).HasColumnName("BusinessUnitId");
            this.Property(t => t.FactureId).HasColumnName("FactureId");
            this.Property(t => t.FactureTypeId).HasColumnName("FactureTypeId");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.FactureDate).HasColumnName("FactureDate");
            this.Property(t => t.FacturePrice).HasColumnName("FacturePrice");
            this.Property(t => t.UserPayment).HasColumnName("UserPayment");
            this.Property(t => t.ProductId).HasColumnName("ProductId");
            this.Property(t => t.ProductPrice).HasColumnName("ProductPrice");
            this.Property(t => t.ProductGroupId).HasColumnName("ProductGroupId");
            this.Property(t => t.ProductName).HasColumnName("ProductName");
            this.Property(t => t.ProductCount).HasColumnName("ProductCount");
            this.Property(t => t.BranchId).HasColumnName("BranchId");
            this.Property(t => t.Creator).HasColumnName("Creator");
            this.Property(t => t.CreatorDate).HasColumnName("CreatorDate");

            // Relationships
            this.HasOptional(t => t.Branch)
                .WithMany(t => t.ClubFactures)
                .HasForeignKey(d => d.BranchId);
            this.HasRequired(t => t.BusinessUnit)
                .WithMany(t => t.ClubFactures)
                .HasForeignKey(d => d.BusinessUnitId);
            this.HasOptional(t => t.FactureType)
                .WithMany(t => t.ClubFactures)
                .HasForeignKey(d => d.FactureTypeId);
            this.HasOptional(t => t.ProductGroup)
                .WithMany(t => t.ClubFactures)
                .HasForeignKey(d => d.ProductGroupId);
            this.HasRequired(t => t.Product)
                .WithMany(t => t.ClubFactures)
                .HasForeignKey(d => d.ProductId);
            this.HasRequired(t => t.User)
                .WithMany(t => t.ClubFactures)
                .HasForeignKey(d => d.Creator);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.ClubFactures1)
                .HasForeignKey(d => d.UserId);

        }
    }
}
