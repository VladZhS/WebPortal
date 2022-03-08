using System;
using System.Collections.Generic;
using System.Linq;

namespace WebPortalServer.Model.WebEnities
{
    public class OrderModel : BaseModel<Order>
    {
        public int Id { get; set; }
        public CustomerModel Customer { get; set; }
        public StatusModel Status { get; set; }
        public decimal TotalCost { get; set; }
        public string Date { get; set; }
        public ICollection<ProductModel> Products { get; set; }
        public bool Archived { get; set; }
        public OrderModel() { }
        public OrderModel(Order order)
        {
            if (order == null)
                throw new ArgumentNullException();
            Id = order.Id;
            Customer = new CustomerModel(order.Customer);
            Status = new StatusModel(order.Status);
            TotalCost = order.OrderProduct.Sum(x => x.Product.Price * x.Quantity);
            Date = order.Date.Date.ToString();
            Products = order.OrderProduct
                .Select(x => x.Product)
                .ConvertModel<Product, ProductModel>();
            Archived = order.Archived;
        }

        public override Order ToEntity(Order order)
        {
            order.Id = Id;
            order.Customer = Customer.ToEntity(order.Customer);
            order.Status = Status.ToEntity(order.Status);
            DateTime date = new DateTime();
            DateTime.TryParse(Date, out date);
            order.Date = date;
            order.CustomerId = Customer.Id;
            order.StatusId = Status.Id;
            order.OrderProduct =
                Products.Select(x => {
                    return new OrderProduct()
                    {
                        OderId = Id,
                        ProductId = x.Id,
                        Quantity = x.Quantity,
                    };
                }).ToList();
            order.Archived = Archived;
            return order;
        }
    }
}
