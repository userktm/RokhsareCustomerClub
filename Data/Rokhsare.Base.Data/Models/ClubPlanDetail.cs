using System;
using System.Collections.Generic;

namespace Rokhsare.Models
{
    public partial class ClubPlanDetail
    {
        public ClubPlanDetail()
        {
            this.ClubPlans = new List<ClubPlan>();
        }

        public int ClubPlanDetailId { get; set; }
        public Nullable<int> PercentOFGiftCredit { get; set; }
        public Nullable<int> LimitUseCreditResort { get; set; }
        public bool LimitUseCreditForce { get; set; }
        public Nullable<int> LimitUserCreditPercent { get; set; }
        public virtual ICollection<ClubPlan> ClubPlans { get; set; }
    }
}
