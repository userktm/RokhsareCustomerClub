using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Rokhsare.Base.Data.Models.Mapping
{
    public class BusinessUnitNetworkMap : EntityTypeConfiguration<BusinessUnitNetwork>
    {
        public BusinessUnitNetworkMap()
        {
            // Primary Key
            this.HasKey(t => t.BusinessUnitNetworkId);

            // Properties
            // Table & Column Mappings
            this.ToTable("BusinessUnitNetworks");
            this.Property(t => t.BusinessUnitNetworkId).HasColumnName("BusinessUnitNetworkId");
            this.Property(t => t.BusinessUnitId).HasColumnName("BusinessUnitId");
            this.Property(t => t.NetworkId).HasColumnName("NetworkId");

            // Relationships
            this.HasRequired(t => t.BusinessUnit)
                .WithMany(t => t.BusinessUnitNetworks)
                .HasForeignKey(d => d.BusinessUnitId);
            this.HasRequired(t => t.Network)
                .WithMany(t => t.BusinessUnitNetworks)
                .HasForeignKey(d => d.NetworkId);

        }
    }
}
