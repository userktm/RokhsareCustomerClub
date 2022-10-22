using System;
using System.Collections.Generic;

namespace Rokhsare.Models
{
    public partial class BusinessUnitNetwork
    {
        public int BusinessUnitNetworkId { get; set; }
        public int BusinessUnitId { get; set; }
        public int NetworkId { get; set; }
        public virtual BusinessUnit BusinessUnit { get; set; }
        public virtual Network Network { get; set; }
    }
}
