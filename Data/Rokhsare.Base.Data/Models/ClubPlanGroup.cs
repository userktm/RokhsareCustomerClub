using System;
using System.Collections.Generic;

namespace Rokhsare.Base.Data.Models
{
    public partial class ClubPlanGroup
    {
        public int ClubPlanGroupId { get; set; }
        public int ClubPlanGroupNumber { get; set; }
        public int ClubPlanId { get; set; }
        public virtual ClubPlan ClubPlan { get; set; }
    }
}
