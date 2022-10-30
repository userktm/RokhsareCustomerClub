using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Rokhsare.Models.Mapping
{
    public class ClubFactureStausMap : EntityTypeConfiguration<ClubFactureStaus>
    {
        public ClubFactureStausMap()
        {
            // Primary Key
            this.HasKey(t => t.ClubFactureStatusId);

            // Properties
            this.Property(t => t.ClubFactureStatusId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ClbFactureStatusName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("ClubFactureStaus");
            this.Property(t => t.ClubFactureStatusId).HasColumnName("ClubFactureStatusId");
            this.Property(t => t.ClbFactureStatusName).HasColumnName("ClbFactureStatusName");
        }
    }
}
