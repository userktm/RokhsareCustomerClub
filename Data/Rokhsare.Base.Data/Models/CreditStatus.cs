using System;
using System.Collections.Generic;

namespace Rokhsare.Models
{
    public partial class CreditStatus
    {
        public CreditStatus()
        {
            this.Credits = new List<Credit>();
        }

        public int CreditStatusId { get; set; }
        public string CreditStatusName { get; set; }
        public virtual ICollection<Credit> Credits { get; set; }
    }
}
