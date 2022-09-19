using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Rokhsare.Base.Data.Models.Mapping
{
    public class ProductTypeMap : EntityTypeConfiguration<ProductType>
    {
        public ProductTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.ProductTypeId);

            // Properties
            this.Property(t => t.ProductTypeName)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("ProductType");
            this.Property(t => t.ProductTypeId).HasColumnName("ProductTypeId");
            this.Property(t => t.ProductTypeName).HasColumnName("ProductTypeName");
        }
    }
}
