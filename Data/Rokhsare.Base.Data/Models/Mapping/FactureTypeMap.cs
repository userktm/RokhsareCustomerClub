using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Rokhsare.Base.Data.Models.Mapping
{
    public class FactureTypeMap : EntityTypeConfiguration<FactureType>
    {
        public FactureTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.FactureTypeId);

            // Properties
            this.Property(t => t.FactureTypeId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.FactureTypeName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("FactureType");
            this.Property(t => t.FactureTypeId).HasColumnName("FactureTypeId");
            this.Property(t => t.FactureTypeName).HasColumnName("FactureTypeName");
        }
    }
}
