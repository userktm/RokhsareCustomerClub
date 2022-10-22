using System;
using System.Collections.Generic;

namespace Rokhsare.Models
{
    public partial class BusinesUnitClubPlan
    {
        public int BusinesUniClubPlanId { get; set; }
        public int BusinesUnitId { get; set; }
        public int ClubPlanId { get; set; }
        public virtual BusinessUnit BusinessUnit { get; set; }
        public virtual ClubPlan ClubPlan { get; set; }
    }
}
