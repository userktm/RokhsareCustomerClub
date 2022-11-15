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
        public int BusinessUnitID { get; set; }
        public Nullable<int> PercentOFGiftCredit { get; set; }
        public Nullable<int> FromCustomerPaymentPrice { get; set; }
        public Nullable<int> ToCustomerPaymentPrice { get; set; }
        public Nullable<int> FromCustomerSumPaymentPrice { get; set; }
        public Nullable<int> ToCustomerSumPaymentPrice { get; set; }
        public virtual BusinessUnit BusinessUnits { get; set; }
        public virtual ICollection<ClubPlan> ClubPlans { get; set; }
    }
}
