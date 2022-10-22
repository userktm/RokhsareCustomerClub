using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Rokhsare.Models.Mapping
{
    public class CurrencyMap : EntityTypeConfiguration<Currency>
    {
        public CurrencyMap()
        {
            // Primary Key
            this.HasKey(t => t.CurrencyId);

            // Properties
            this.Property(t => t.CurrencyId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CurrencyName)
                .HasMaxLength(100);

            this.Property(t => t.CurrencyComment)
                .HasMaxLength(300);

            // Table & Column Mappings
            this.ToTable("Currency");
            this.Property(t => t.CurrencyId).HasColumnName("CurrencyId");
            this.Property(t => t.CurrencyName).HasColumnName("CurrencyName");
            this.Property(t => t.CurrencyCoefficient).HasColumnName("CurrencyCoefficient");
            this.Property(t => t.CurrencyComment).HasColumnName("CurrencyComment");
        }
    }
}
