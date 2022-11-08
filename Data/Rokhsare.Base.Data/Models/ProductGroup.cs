using System;
using System.Collections.Generic;

namespace Rokhsare.Models
{
    public partial class ProductGroup
    {
        public ProductGroup()
        {
            this.Products = new List<Product>();
        }

        public int ProductGroupId { get; set; }
        public int BusinessUnitId { get; set; }
        public string ProductGroupName { get; set; }
        public virtual BusinessUnit BusinessUnit { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
