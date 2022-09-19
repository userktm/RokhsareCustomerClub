using System;
using System.Collections.Generic;

namespace Rokhsare.Base.Data.Models
{
    public partial class ClubPlan
    {
        public ClubPlan()
        {
            this.BusinesUnitClubPlans = new List<BusinesUnitClubPlan>();
            this.Cards = new List<Card>();
            this.ClubPlanGroups = new List<ClubPlanGroup>();
            this.DefaultClubPlans = new List<DefaultClubPlan>();
            this.UserPlans = new List<UserPlan>();
        }

        public int ClubPlanId { get; set; }
        public int ClubPlanDetailId { get; set; }
        public string ClubPlanName { get; set; }
        public string ClubPlanDescription { get; set; }
        public virtual ICollection<BusinesUnitClubPlan> BusinesUnitClubPlans { get; set; }
        public virtual ICollection<Card> Cards { get; set; }
        public virtual ClubPlanDetail ClubPlanDetail { get; set; }
        public virtual ICollection<ClubPlanGroup> ClubPlanGroups { get; set; }
        public virtual ICollection<DefaultClubPlan> DefaultClubPlans { get; set; }
        public virtual ICollection<UserPlan> UserPlans { get; set; }
    }
}
