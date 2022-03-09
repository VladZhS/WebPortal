using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace WebPortalServer
{
    public partial class Product
    {
        public Product()
        {
            OrderProduct = new HashSet<OrderProduct>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public int CategoryId { get; set; }
        public int SizeId { get; set; }

        public virtual ProductCategory Category { get; set; }
        public virtual ProductSize Size { get; set; }
        public virtual ICollection<OrderProduct> OrderProduct { get; set; }
    }
}
