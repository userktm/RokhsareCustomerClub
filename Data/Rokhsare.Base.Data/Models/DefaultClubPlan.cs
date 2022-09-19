using System;
using System.Collections.Generic;

namespace Rokhsare.Base.Data.Models
{
    public partial class DefaultClubPlan
    {
        public int DefaulteClubPlanId { get; set; }
        public int BusinessUnitId { get; set; }
        public int ClubPlanId { get; set; }
        public virtual BusinessUnit BusinessUnit { get; set; }
        public virtual ClubPlan ClubPlan { get; set; }
    }
}
