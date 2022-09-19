using System;
using System.Collections.Generic;

namespace Rokhsare.Base.Data.Models
{
    public partial class Product
    {
        public Product()
        {
            this.ClubFactures = new List<ClubFacture>();
        }

        public long ProductId { get; set; }
        public int BusinessUnitId { get; set; }
        public int ProductTypeId { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public virtual BusinessUnit BusinessUnit { get; set; }
        public virtual ICollection<ClubFacture> ClubFactures { get; set; }
        public virtual ProductType ProductType { get; set; }
    }
}
