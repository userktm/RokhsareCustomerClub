using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Rokhsare.Models.Mapping
{
    public class SMSTemplateTypeMap : EntityTypeConfiguration<SMSTemplateType>
    {
        public SMSTemplateTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.SMSTemplateTypeId);

            // Properties
            this.Property(t => t.SMSTemplateTypeName)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("SMSTemplateType");
            this.Property(t => t.SMSTemplateTypeId).HasColumnName("SMSTemplateTypeId");
            this.Property(t => t.SMSTemplateTypeName).HasColumnName("SMSTemplateTypeName");
        }
    }
}
