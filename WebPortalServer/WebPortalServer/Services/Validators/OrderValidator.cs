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
                if (order.Customer == null)
                    error.AddError("customer", "Customer can't be null");
                else
                {
                    if (order.Customer.Name == null)
                        error.AddError("customer", "Customer's name can't be null");
                    else if (order.Customer.Name == "")
                        error.AddError("customer", "Customer's name can't be empty");
                    if (order.Customer.Address == null)
                        error.AddError("address", "Customer's address can't be null");
                    else if (order.Customer.Address == "")
                        error.AddError("address", "Customer's address can't be empty");
                }
                if (order.Status == null)
                    error.AddError("status", "Status can't be null");
                else if (context.OrderStatus.FirstOrDefault(x => x.Id == order.Status.Id) == null)
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
