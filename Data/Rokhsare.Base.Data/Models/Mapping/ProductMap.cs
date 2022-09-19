using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Rokhsare.Base.Data.Models.Mapping
{
    public class ProductMap : EntityTypeConfiguration<Product>
    {
        public ProductMap()
        {
            // Primary Key
            this.HasKey(t => t.ProductId);

            // Properties
            this.Property(t => t.ProductName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.ProductCode)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Products");
            this.Property(t => t.ProductId).HasColumnName("ProductId");
            this.Property(t => t.BusinessUnitId).HasColumnName("BusinessUnitId");
            this.Property(t => t.ProductTypeId).HasColumnName("ProductTypeId");
            this.Property(t => t.ProductName).HasColumnName("ProductName");
            this.Property(t => t.ProductCode).HasColumnName("ProductCode");

            // Relationships
            this.HasRequired(t => t.BusinessUnit)
                .WithMany(t => t.Products)
                .HasForeignKey(d => d.BusinessUnitId);
            this.HasRequired(t => t.ProductType)
                .WithMany(t => t.Products)
                .HasForeignKey(d => d.ProductTypeId);

        }
    }
}
