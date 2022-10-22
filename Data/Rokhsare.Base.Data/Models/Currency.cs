using System;
using System.Collections.Generic;

namespace Rokhsare.Models
{
    public partial class Currency
    {
        public int CurrencyId { get; set; }
        public string CurrencyName { get; set; }
        public int CurrencyCoefficient { get; set; }
        public string CurrencyComment { get; set; }
    }
}
