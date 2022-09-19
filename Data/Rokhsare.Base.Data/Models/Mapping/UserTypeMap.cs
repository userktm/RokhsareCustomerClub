using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Rokhsare.Base.Data.Models.Mapping
{
    public class UserTypeMap : EntityTypeConfiguration<UserType>
    {
        public UserTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.UserTypeID);

            // Properties
            this.Property(t => t.UserTypeID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.UserTypeName)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("UserType");
            this.Property(t => t.UserTypeID).HasColumnName("UserTypeID");
            this.Property(t => t.UserTypeName).HasColumnName("UserTypeName");
        }
    }
}
