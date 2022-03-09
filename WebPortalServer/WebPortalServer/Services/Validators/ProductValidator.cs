using System;
using System.Linq;
using WebPortalServer.Model.WebEnities;

namespace WebPortalServer.Services.Validators
{
    public class ProductValidator : IProductValidator
    {
        private readonly WebPortalDBContext context;

        public ProductValidator(WebPortalDBContext context)
        {
            this.context = context;
        }

        public ModelError IsValid(ProductModel product)
        {
            ModelError error = new ModelError();

            if (product == null)
                error.AddError("product", "Product can't be null");
            else
            {
                if (product.Name == null)
                    error.AddError("name", "Name can't be null");
                else if (product.Name == "")
                    error.AddError("name", "Name can't be empty");
                if (product.Price <= 0)
                    error.AddError("price", "Price can't be less or equal to zero");
                else if (product.Quantity < 0)
                    error.AddError("quantity", "Quantity can't be less than zero");
                if (product.Category == null)
                    error.AddError("category", "Category can't be null");
                else if (context.ProductCategory.FirstOrDefault(x => x.Id == product.Category.Id) == null)
                    error.AddError("category", "Invalid category");
                if (product.Size == null)
                    error.AddError("size", "Size can't be null");
                else if (context.ProductSize.FirstOrDefault(x => x.Id == product.Size.Id) == null)
                    error.AddError("size", "Invalid size");
                if (product.CreationDate == null)
                    error.AddError("date", "Date can't be null");
                else
                {
                    DateTime date = new DateTime();
                    if (!DateTime.TryParse(product.CreationDate, out date))
                        error.AddError("date", "Ivalid date");
                }
            }

            return error;
        }
    }
}
