using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Rokhsare.Base.Data.Models.Mapping
{
    public class CreditTypeMap : EntityTypeConfiguration<CreditType>
    {
        public CreditTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.CredityTypeId);

            // Properties
            // Table & Column Mappings
            this.ToTable("CreditType");
            this.Property(t => t.CredityTypeId).HasColumnName("CredityTypeId");
        }
    }
}
