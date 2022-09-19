using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Rokhsare.Base.Data.Models.Mapping
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            // Primary Key
            this.HasKey(t => t.UserID);

            // Properties
            this.Property(t => t.NationalNumber)
                .HasMaxLength(10);

            this.Property(t => t.UserCode)
                .HasMaxLength(50);

            this.Property(t => t.FullName)
                .IsRequired()
                .HasMaxLength(256);

            this.Property(t => t.AvatarImage)
                .HasMaxLength(400);

            this.Property(t => t.MobileNumber)
                .IsRequired()
                .HasMaxLength(300);

            this.Property(t => t.PhoneNumber)
                .HasMaxLength(100);

            this.Property(t => t.BankCardNumber)
                .HasMaxLength(16);

            this.Property(t => t.UserName)
                .HasMaxLength(256);

            this.Property(t => t.Email)
                .HasMaxLength(256);

            this.Property(t => t.ReferrerCode)
                .HasMaxLength(6);

            // Table & Column Mappings
            this.ToTable("Users");
            this.Property(t => t.UserID).HasColumnName("UserID");
            this.Property(t => t.BusinessUnitId).HasColumnName("BusinessUnitId");
            this.Property(t => t.NationalNumber).HasColumnName("NationalNumber");
            this.Property(t => t.UserCode).HasColumnName("UserCode");
            this.Property(t => t.CardID).HasColumnName("CardID");
            this.Property(t => t.FullName).HasColumnName("FullName");
            this.Property(t => t.AvatarImage).HasColumnName("AvatarImage");
            this.Property(t => t.MobileNumber).HasColumnName("MobileNumber");
            this.Property(t => t.PhoneNumber).HasColumnName("PhoneNumber");
            this.Property(t => t.BankCardNumber).HasColumnName("BankCardNumber");
            this.Property(t => t.Address).HasColumnName("Address");
            this.Property(t => t.UserName).HasColumnName("UserName");
            this.Property(t => t.Password).HasColumnName("Password");
            this.Property(t => t.BirthDate).HasColumnName("BirthDate");
            this.Property(t => t.MarriageDate).HasColumnName("MarriageDate");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.EmailConfirmed).HasColumnName("EmailConfirmed");
            this.Property(t => t.Active).HasColumnName("Active");
            this.Property(t => t.UserTypeID).HasColumnName("UserTypeID");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.Creator).HasColumnName("Creator");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Modifier).HasColumnName("Modifier");
            this.Property(t => t.SecurityStamp).HasColumnName("SecurityStamp");
            this.Property(t => t.MobileNumberConfirmed).HasColumnName("MobileNumberConfirmed");
            this.Property(t => t.LockoutEndDateUtc).HasColumnName("LockoutEndDateUtc");
            this.Property(t => t.LockoutEnabled).HasColumnName("LockoutEnabled");
            this.Property(t => t.AccessFailedCount).HasColumnName("AccessFailedCount");
            this.Property(t => t.ReferrerCode).HasColumnName("ReferrerCode");
            this.Property(t => t.ReferrerUserId).HasColumnName("ReferrerUserId");
            this.Property(t => t.IsVerifiedAvatar).HasColumnName("IsVerifiedAvatar");
            this.Property(t => t.VerifiedAvatarBy).HasColumnName("VerifiedAvatarBy");

            // Relationships
            this.HasRequired(t => t.BusinessUnit)
                .WithMany(t => t.Users)
                .HasForeignKey(d => d.BusinessUnitId);
            this.HasOptional(t => t.Card)
                .WithMany(t => t.Users)
                .HasForeignKey(d => d.CardID);
            this.HasOptional(t => t.User1)
                .WithMany(t => t.Users1)
                .HasForeignKey(d => d.Creator);
            this.HasOptional(t => t.User2)
                .WithMany(t => t.Users11)
                .HasForeignKey(d => d.Modifier);
            this.HasOptional(t => t.User3)
                .WithMany(t => t.Users12)
                .HasForeignKey(d => d.ReferrerUserId);
            this.HasOptional(t => t.User4)
                .WithMany(t => t.Users13)
                .HasForeignKey(d => d.VerifiedAvatarBy);
            this.HasRequired(t => t.UserType)
                .WithMany(t => t.Users)
                .HasForeignKey(d => d.UserTypeID);

        }
    }
}
