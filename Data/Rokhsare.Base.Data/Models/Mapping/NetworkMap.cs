using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Rokhsare.Base.Data.Models.Mapping
{
    public class NetworkMap : EntityTypeConfiguration<Network>
    {
        public NetworkMap()
        {
            // Primary Key
            this.HasKey(t => t.NetworkId);

            // Properties
            this.Property(t => t.NetworkId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.NetworkName)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("Networks");
            this.Property(t => t.NetworkId).HasColumnName("NetworkId");
            this.Property(t => t.NetworkName).HasColumnName("NetworkName");
        }
    }
}
