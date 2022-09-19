using System;
using System.Collections.Generic;

namespace Rokhsare.Base.Data.Models
{
    public partial class ProductType
    {
        public ProductType()
        {
            this.Products = new List<Product>();
        }

        public int ProductTypeId { get; set; }
        public string ProductTypeName { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
