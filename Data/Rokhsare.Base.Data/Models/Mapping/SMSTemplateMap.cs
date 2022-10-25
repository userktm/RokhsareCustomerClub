using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Rokhsare.Models.Mapping
{
    public class SMSTemplateMap : EntityTypeConfiguration<SMSTemplate>
    {
        public SMSTemplateMap()
        {
            // Primary Key
            this.HasKey(t => t.SMSTemplateId);

            // Properties
            this.Property(t => t.SMSTemplateName)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("SMSTemplate");
            this.Property(t => t.SMSTemplateId).HasColumnName("SMSTemplateId");
            this.Property(t => t.ClubPlanId).HasColumnName("ClubPlanId");
            this.Property(t => t.SMSTemplateName).HasColumnName("SMSTemplateName");
            this.Property(t => t.SMSTemplateTypeId).HasColumnName("SMSTemplateTypeId");

            // Relationships
            this.HasRequired(t => t.ClubPlan)
                .WithMany(t => t.SMSTemplates)
                .HasForeignKey(d => d.ClubPlanId);

        }
    }
}
