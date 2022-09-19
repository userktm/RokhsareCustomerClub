using System;
using System.Collections.Generic;

namespace Rokhsare.Base.Data.Models
{
    public partial class Network
    {
        public Network()
        {
            this.BusinessUnitNetworks = new List<BusinessUnitNetwork>();
        }

        public int NetworkId { get; set; }
        public string NetworkName { get; set; }
        public virtual ICollection<BusinessUnitNetwork> BusinessUnitNetworks { get; set; }
    }
}
