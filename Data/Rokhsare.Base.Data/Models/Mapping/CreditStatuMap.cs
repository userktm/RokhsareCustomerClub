using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Rokhsare.Base.Data.Models.Mapping
{
    public class CreditStatuMap : EntityTypeConfiguration<CreditStatu>
    {
        public CreditStatuMap()
        {
            // Primary Key
            this.HasKey(t => t.CreditStatusId);

            // Properties
            this.Property(t => t.CreditStatusId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CreditStatusName)
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("CreditStatus");
            this.Property(t => t.CreditStatusId).HasColumnName("CreditStatusId");
            this.Property(t => t.CreditStatusName).HasColumnName("CreditStatusName");
        }
    }
}
