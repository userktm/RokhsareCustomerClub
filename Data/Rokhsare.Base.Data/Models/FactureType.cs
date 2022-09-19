using System;
using System.Collections.Generic;

namespace Rokhsare.Base.Data.Models
{
    public partial class FactureType
    {
        public FactureType()
        {
            this.ClubFactures = new List<ClubFacture>();
        }

        public int FactureTypeId { get; set; }
        public string FactureTypeName { get; set; }
        public virtual ICollection<ClubFacture> ClubFactures { get; set; }
    }
}
