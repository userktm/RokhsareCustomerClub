using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Rokhsare.Models.Mapping
{
    public class ProductGroupMap : EntityTypeConfiguration<ProductGroup>
    {
        public ProductGroupMap()
        {
            // Primary Key
            this.HasKey(t => t.ProductGroupId);

            // Properties
            this.Property(t => t.ProductGroupId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ProductGroupName)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("ProductGroups");
            this.Property(t => t.ProductGroupId).HasColumnName("ProductGroupId");
            this.Property(t => t.BusinessUnitId).HasColumnName("BusinessUnitId");
            this.Property(t => t.ProductGroupName).HasColumnName("ProductGroupName");

            // Relationships
            this.HasRequired(t => t.BusinessUnit)
                .WithMany(t => t.ProductGroups)
                .HasForeignKey(d => d.BusinessUnitId);

        }
    }
}
