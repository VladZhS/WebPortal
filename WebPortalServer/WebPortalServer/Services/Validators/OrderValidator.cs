using System;
using System.Linq;
using WebPortalServer.Model.WebEnities;

namespace WebPortalServer.Services.Validators
{
    public class OrderValidator : IOrderValidator
    {
        private readonly WebPortalDBContext context;
        private readonly IProductValidator validator;

        public OrderValidator(WebPortalDBContext context, IProductValidator validator)
        {
            this.context = context;
            this.validator = validator;
        }
        public ModelError IsValid(OrderModel order)
        {
            ModelError error = new ModelError();

            if (order == null)
                error.AddError("order", "Order can't be null");
            else
            {
                if (order.Products == null)
                    error.AddError("products", "Products can't be null");
                else if (order.Products.Count == 0)
                    error.AddError("products", "No products");

                var customerId = order.Customer != null ? order.Customer.Id : order.CustomerId;
                if (customerId == 0)
                    error.AddError("customer", "Customer can't be null");
                else
                {                   
                    if (context.Customer.FirstOrDefault(x => x.Id == customerId) == null)
                        error.AddError("customer", "Invalid customer id");
                }
                if (order.CreationDate == null)
                    error.AddError("date", "Date can't be null");
                else
                {
                    DateTime date = new DateTime();
                    if (!DateTime.TryParse(order.CreationDate, out date))
                        error.AddError("date", "Ivalid date");
                }
                var statusId = order.Status != null ? order.Status.Id : order.StatusId;
                if (statusId == 0)
                    error.AddError("status", "Status can't be null");
                else if (context.OrderStatus.FirstOrDefault(x => x.Id == statusId) == null)
                    error.AddError("status", "Invalid status");
                if (order.Products == null)
                    error.AddError("products", "Products can't be null");
                else
                {
                    foreach (var item in order.Products)
                    {
                        var product = context.Product.FirstOrDefault(x => x.Id == item.Id);
                        if (product == null)
                            error.AddError("product", "Product can't be null");
                        else if (context.Product.FirstOrDefault(x => x.Id == item.Id) == null)
                            error.AddError("product", "Invalid product id");
                        else
                        {
                            ModelError productError = validator.IsValid(item);
                            foreach (var e in productError)
                            {
                                error.AddError(e.Key + $"\"{item.Name}\"", e.Value);
                            }
                        }

                    }
                }
            }

            return error;
        }
    }
}
