using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Rokhsare.Models.Mapping
{
    public class CreditTypeMap : EntityTypeConfiguration<CreditType>
    {
        public CreditTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.CredityTypeId);

            // Properties
            this.Property(t => t.CreditTypeName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CreditType");
            this.Property(t => t.CredityTypeId).HasColumnName("CredityTypeId");
            this.Property(t => t.CreditTypeName).HasColumnName("CreditTypeName");
        }
    }
}
