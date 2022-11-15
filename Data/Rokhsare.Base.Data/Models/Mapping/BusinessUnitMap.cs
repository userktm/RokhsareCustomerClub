using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Rokhsare.Models.Mapping
{
    public class BusinessUnitMap : EntityTypeConfiguration<BusinessUnit>
    {
        public BusinessUnitMap()
        {
            // Primary Key
            this.HasKey(t => t.BusinessUnitId);

            // Properties
            this.Property(t => t.UnitName)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.Mobile)
                .HasMaxLength(150);

            this.Property(t => t.Phone)
                .HasMaxLength(150);

            this.Property(t => t.Address)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.InstagramId)
                .HasMaxLength(50);

            this.Property(t => t.Description)
                .HasMaxLength(3000);

            this.Property(t => t.AdviseNumber)
                .HasMaxLength(15);

            // Table & Column Mappings
            this.ToTable("BusinessUnit");
            this.Property(t => t.BusinessUnitId).HasColumnName("BusinessUnitId");
            this.Property(t => t.UnitName).HasColumnName("UnitName");
            this.Property(t => t.Mobile).HasColumnName("Mobile");
            this.Property(t => t.Phone).HasColumnName("Phone");
            this.Property(t => t.Address).HasColumnName("Address");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.Creator).HasColumnName("Creator");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Modifier).HasColumnName("Modifier");
            this.Property(t => t.Active).HasColumnName("Active");
            this.Property(t => t.SmsNotification).HasColumnName("SmsNotification");
            this.Property(t => t.SmsApiKey).HasColumnName("SmsApiKey");
            this.Property(t => t.CurrencyId).HasColumnName("CurrencyId");
            this.Property(t => t.JobId).HasColumnName("JobId");
            this.Property(t => t.InstagramId).HasColumnName("InstagramId");
            this.Property(t => t.IsUpdating).HasColumnName("IsUpdating");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.AdviseNumber).HasColumnName("AdviseNumber");
            this.Property(t => t.LimitUseCreditResort).HasColumnName("LimitUseCreditResort");
            this.Property(t => t.LimitUseCreditForce).HasColumnName("LimitUseCreditForce");
            this.Property(t => t.LimitUserCreditPercent).HasColumnName("LimitUserCreditPercent");
            this.Property(t => t.ExpireDate).HasColumnName("ExpireDate");

            // Relationships
            this.HasOptional(t => t.User)
                .WithMany(t => t.BusinessUnits)
                .HasForeignKey(d => d.Creator);
            this.HasOptional(t => t.User1)
                .WithMany(t => t.BusinessUnits1)
                .HasForeignKey(d => d.Modifier);

        }
    }
}
