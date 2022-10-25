using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Rokhsare.Models.Mapping
{
    public class SMSTemplateTokenMap : EntityTypeConfiguration<SMSTemplateToken>
    {
        public SMSTemplateTokenMap()
        {
            // Primary Key
            this.HasKey(t => t.SMSTemplateTokenId);

            // Properties
            this.Property(t => t.SMSTemplateTokenName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.SMSTemplateTokenDesc)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("SMSTemplateTokens");
            this.Property(t => t.SMSTemplateTokenId).HasColumnName("SMSTemplateTokenId");
            this.Property(t => t.SMSTemplateId).HasColumnName("SMSTemplateId");
            this.Property(t => t.SMSTemplateTokenName).HasColumnName("SMSTemplateTokenName");
            this.Property(t => t.SMSTemplateTokenDesc).HasColumnName("SMSTemplateTokenDesc");

            // Relationships
            this.HasRequired(t => t.SMSTemplate)
                .WithMany(t => t.SMSTemplateTokens)
                .HasForeignKey(d => d.SMSTemplateId);

        }
    }
}
