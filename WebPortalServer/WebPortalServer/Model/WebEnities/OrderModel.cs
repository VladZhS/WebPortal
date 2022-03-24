using System;
using System.Collections.Generic;
using System.Linq;

namespace WebPortalServer.Model.WebEnities
{
    public class OrderModel : BaseModel<Order>
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public CustomerModel Customer { get; set; }
        public int StatusId { get; set; }
        public StatusModel Status { get; set; }
        public decimal TotalCost { get; set; }
        public string CreationDate { get; set; }
        public string Description { get; set; }
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

            CreationDate = order.CreationDate.Date.ToString();
            Description = order.Description;
            Products = order.OrderProduct
                .Select(x =>
                {
                    x.Product.Quantity = x.Quantity;
                    return x.Product;
                })
                .ConvertModel<Product, ProductModel>();

            Archived = order.Archived;
        }

        public override Order ToEntity(Order order)
        {
            order.Id = Id;
            order.Customer = Customer?.ToEntity(order.Customer) ?? order.Customer;
            order.CustomerId = Customer?.Id ?? CustomerId;
            order.Status = Status?.ToEntity(order.Status) ?? order.Status;
            order.StatusId = Status?.Id ?? StatusId;
            DateTime date = new DateTime();
            DateTime.TryParse(CreationDate, out date);
            order.CreationDate = date;
            order.Description = Description;
            order.OrderProduct =
                Products.Select(x => {
                    return new OrderProduct()
                    {
                        OderId = Id,
                        ProductId = x.Id,
                        Quantity = x.Quantity,
                    };
                }).ToList();

            TotalCost = order.OrderProduct.Sum(x => x.Product.Price * x.Quantity);
            order.Archived = Archived;
            return order;
        }
    }
}
