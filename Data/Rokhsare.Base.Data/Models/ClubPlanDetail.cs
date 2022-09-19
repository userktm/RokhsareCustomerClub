using System;
using System.Collections.Generic;

namespace Rokhsare.Base.Data.Models
{
    public partial class ClubPlanDetail
    {
        public ClubPlanDetail()
        {
            this.ClubPlans = new List<ClubPlan>();
        }

        public int ClubPlanDetailId { get; set; }
        public virtual ICollection<ClubPlan> ClubPlans { get; set; }
    }
}
