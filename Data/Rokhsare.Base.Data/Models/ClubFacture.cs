using System;
using System.Collections.Generic;

namespace Rokhsare.Models
{
    public partial class ClubFacture
    {
        public ClubFacture()
        {
            this.Credits = new List<Credit>();
        }

        public long ClubFactureId { get; set; }
        public int BusinessUnitId { get; set; }
        public Nullable<long> FactureId { get; set; }
        public Nullable<int> FactureTypeId { get; set; }
        public long UserId { get; set; }
        public System.DateTime FactureDate { get; set; }
        public int FacturePrice { get; set; }
        public int UserPayment { get; set; }
        public long ProductId { get; set; }
        public int ProductPrice { get; set; }
        public int ProductCount { get; set; }
        public Nullable<int> BranchId { get; set; }
        public long Creator { get; set; }
        public System.DateTime CreatorDate { get; set; }
        public int ClubFactureStatusId { get; set; }
        public virtual ClubFactureStaus ClubFactureStaus { get; set; }
        public virtual Branch Branch { get; set; }
        public virtual BusinessUnit BusinessUnit { get; set; }
        public virtual FactureType FactureType { get; set; }
        public virtual Product Product { get; set; }
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
        public virtual ICollection<Credit> Credits { get; set; }
    }
}
