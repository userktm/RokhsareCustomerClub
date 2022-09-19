using System;
using System.Collections.Generic;

namespace Rokhsare.Base.Data.Models
{
    public partial class Branch
    {
        public Branch()
        {
            this.ClubFactures = new List<ClubFacture>();
        }

        public int BranchId { get; set; }
        public int BusinessUnitId { get; set; }
        public string BranchName { get; set; }
        public virtual BusinessUnit BusinessUnit { get; set; }
        public virtual ICollection<ClubFacture> ClubFactures { get; set; }
    }
}
