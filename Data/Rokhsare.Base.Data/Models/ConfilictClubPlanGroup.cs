using System;
using System.Collections.Generic;

namespace Rokhsare.Models
{
    public partial class ConfilictClubPlanGroup
    {
        public int ConfilictClubPlanGroupId { get; set; }
        public int ConfilictClubPlanGroupNumber { get; set; }
        public int ClubPlanId { get; set; }
        public virtual ClubPlan ClubPlan { get; set; }
    }
}
