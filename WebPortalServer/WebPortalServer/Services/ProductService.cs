using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WebPortalServer.Model.WebEnities;

namespace WebPortalServer.Services
{
    public class ProductService : IProductService
    {
        private readonly WebPortalDBContext context;

        public ProductService(WebPortalDBContext context)
        {
            this.context = context;
        }

        public Product CreateProduct(ProductModel model)
        {
            var product = model.ToEntity(
                new Product()
                {
                    Category = new ProductCategory(),
                    Size = new ProductSize()
                });

            product.Category = null;
            product.Size = null;

            context.Product.Add(product);
            
            context.SaveChanges();

            return product;
        }
        public Product UpdateProduct(ProductModel model)
        {
            var product = context.Product.FirstOrDefault(x => x.Id == model.Id);
            if (product == null)
                throw new InvalidOperationException();

            product = model.ToEntitySafe(product);
            context.Product.Update(product);

            context.SaveChanges();

            return product;
        }

        public Product DeleteProduct(int id)
        {
            var product = context.Product.FirstOrDefault(x => x.Id == id);
            if (product == null)
                throw new InvalidOperationException();

            context.Product.Remove(product);
            context.SaveChanges();

            return product;
        }

        public IEnumerable<ProductModel> ConverList(IEnumerable<Product> collection)
        {
            return collection.ConvertModel<Product, ProductModel>();
        }
    }
}
