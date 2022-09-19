using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Rokhsare.Base.Data.Models.Mapping
{
    public class BranchMap : EntityTypeConfiguration<Branch>
    {
        public BranchMap()
        {
            // Primary Key
            this.HasKey(t => t.BranchId);

            // Properties
            this.Property(t => t.BranchId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.BranchName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Branches");
            this.Property(t => t.BranchId).HasColumnName("BranchId");
            this.Property(t => t.BusinessUnitId).HasColumnName("BusinessUnitId");
            this.Property(t => t.BranchName).HasColumnName("BranchName");

            // Relationships
            this.HasRequired(t => t.BusinessUnit)
                .WithMany(t => t.Branches)
                .HasForeignKey(d => d.BusinessUnitId);

        }
    }
}
