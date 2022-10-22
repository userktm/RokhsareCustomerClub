using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Rokhsare.Models.Mapping
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
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Branches");
            this.Property(t => t.BranchId).HasColumnName("BranchId");
            this.Property(t => t.BusinessUnitId).HasColumnName("BusinessUnitId");
            this.Property(t => t.BranchName).HasColumnName("BranchName");
            this.Property(t => t.BranchNumber).HasColumnName("BranchNumber");

            // Relationships
            this.HasRequired(t => t.BusinessUnit)
                .WithMany(t => t.Branches)
                .HasForeignKey(d => d.BusinessUnitId);

        }
    }
}
