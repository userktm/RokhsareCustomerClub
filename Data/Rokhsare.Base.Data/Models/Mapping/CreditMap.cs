using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Rokhsare.Base.Data.Models.Mapping
{
    public class CreditMap : EntityTypeConfiguration<Credit>
    {
        public CreditMap()
        {
            // Primary Key
            this.HasKey(t => t.CreditId);

            // Properties
            // Table & Column Mappings
            this.ToTable("Credit");
            this.Property(t => t.CreditId).HasColumnName("CreditId");
            this.Property(t => t.CreditAmount).HasColumnName("CreditAmount");
            this.Property(t => t.ClubFactureId).HasColumnName("ClubFactureId");
            this.Property(t => t.CreditStartDate).HasColumnName("CreditStartDate");
            this.Property(t => t.CreditEndDate).HasColumnName("CreditEndDate");
            this.Property(t => t.CreditExpireDate).HasColumnName("CreditExpireDate");
            this.Property(t => t.TotalCreditNow).HasColumnName("TotalCreditNow");
            this.Property(t => t.CreditTypeId).HasColumnName("CreditTypeId");
            this.Property(t => t.CreditStatusId).HasColumnName("CreditStatusId");
            this.Property(t => t.CreditComment).HasColumnName("CreditComment");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.Creator).HasColumnName("Creator");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.Modifire).HasColumnName("Modifire");
            this.Property(t => t.ModifireDate).HasColumnName("ModifireDate");

            // Relationships
            this.HasOptional(t => t.ClubFacture)
                .WithMany(t => t.Credits)
                .HasForeignKey(d => d.ClubFactureId);
            this.HasRequired(t => t.CreditStatu)
                .WithMany(t => t.Credits)
                .HasForeignKey(d => d.CreditStatusId);
            this.HasRequired(t => t.Credit2)
                .WithOptional(t => t.Credit1);
            this.HasRequired(t => t.User)
                .WithMany(t => t.Credits)
                .HasForeignKey(d => d.Creator);
            this.HasOptional(t => t.User1)
                .WithMany(t => t.Credits1)
                .HasForeignKey(d => d.Modifire);
            this.HasOptional(t => t.User2)
                .WithMany(t => t.Credits2)
                .HasForeignKey(d => d.UserId);

        }
    }
}
