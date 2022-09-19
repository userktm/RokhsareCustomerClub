using System;
using System.Collections.Generic;

namespace Rokhsare.Base.Data.Models
{
    public partial class CreditStatu
    {
        public CreditStatu()
        {
            this.Credits = new List<Credit>();
        }

        public int CreditStatusId { get; set; }
        public string CreditStatusName { get; set; }
        public virtual ICollection<Credit> Credits { get; set; }
    }
}
