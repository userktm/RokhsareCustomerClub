using System;
using System.Collections.Generic;

namespace Rokhsare.Models
{
    public partial class ClubFactureStaus
    {
        public ClubFactureStaus()
        {
            this.ClubFactures = new List<ClubFacture>();
        }

        public int ClubFactureStatusId { get; set; }
        public string ClbFactureStatusName { get; set; }
        public virtual ICollection<ClubFacture> ClubFactures { get; set; }
    }
}
