using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace WebPortalServer
{
    public partial class Order
    {
        public Order()
        {
            OrderProduct = new HashSet<OrderProduct>();
        }

        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int StatusId { get; set; }
        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual OrderStatus Status { get; set; }
        public virtual ICollection<OrderProduct> OrderProduct { get; set; }
    }
}
