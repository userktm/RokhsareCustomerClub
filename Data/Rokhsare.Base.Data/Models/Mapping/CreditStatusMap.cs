using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Rokhsare.Models.Mapping
{
    public class CreditStatusMap : EntityTypeConfiguration<CreditStatus>
    {
        public CreditStatusMap()
        {
            // Primary Key
            this.HasKey(t => t.CreditStatusId);

            // Properties
            this.Property(t => t.CreditStatusId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CreditStatusName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CreditStatus");
            this.Property(t => t.CreditStatusId).HasColumnName("CreditStatusId");
            this.Property(t => t.CreditStatusName).HasColumnName("CreditStatusName");
        }
    }
}
