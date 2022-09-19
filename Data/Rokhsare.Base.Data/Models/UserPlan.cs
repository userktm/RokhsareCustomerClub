using System;
using System.Collections.Generic;

namespace Rokhsare.Base.Data.Models
{
    public partial class UserPlan
    {
        public long UserPlanId { get; set; }
        public long UserId { get; set; }
        public int ClubPlanId { get; set; }
        public Nullable<System.DateTime> ExpireDate { get; set; }
        public virtual ClubPlan ClubPlan { get; set; }
        public virtual User User { get; set; }
    }
}
