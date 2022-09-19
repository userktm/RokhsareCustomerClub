using System;
using System.Collections.Generic;

namespace Rokhsare.Base.Data.Models
{
    public partial class BusinesUnitClubPlan
    {
        public int BusinesUniClubPlanId { get; set; }
        public int BusinesUniId { get; set; }
        public int ClubPlanId { get; set; }
        public virtual BusinessUnit BusinessUnit { get; set; }
        public virtual ClubPlan ClubPlan { get; set; }
    }
}
