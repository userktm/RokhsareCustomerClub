using System;
using System.Collections.Generic;

namespace Rokhsare.Models
{
    public partial class SMSTemplate
    {
        public SMSTemplate()
        {
            this.SMSTemplateTokens = new List<SMSTemplateToken>();
        }

        public int SMSTemplateId { get; set; }
        public int ClubPlanId { get; set; }
        public string SMSTemplateName { get; set; }
        public int SMSTemplateTypeId { get; set; }
        public virtual ClubPlan ClubPlan { get; set; }
        public virtual ICollection<SMSTemplateToken> SMSTemplateTokens { get; set; }
    }
}
