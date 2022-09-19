using System;
using System.Collections.Generic;

namespace Rokhsare.Base.Data.Models
{
    public partial class Card
    {
        public Card()
        {
            this.Users = new List<User>();
        }

        public long CardId { get; set; }
        public Nullable<int> ClubPlanId { get; set; }
        public string CardNumber { get; set; }
        public System.DateTime CreateDate { get; set; }
        public long Creator { get; set; }
        public Nullable<System.DateTime> ExpireDate { get; set; }
        public virtual ClubPlan ClubPlan { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
