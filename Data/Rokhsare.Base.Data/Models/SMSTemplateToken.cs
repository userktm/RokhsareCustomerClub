using System;
using System.Collections.Generic;

namespace Rokhsare.Models
{
    public partial class SMSTemplateToken
    {
        public int SMSTemplateTokenId { get; set; }
        public int SMSTemplateId { get; set; }
        public string SMSTemplateTokenName { get; set; }
        public string SMSTemplateTokenDesc { get; set; }
        public virtual SMSTemplate SMSTemplate { get; set; }
    }
}
