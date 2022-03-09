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

            var category = context.ProductCategory.
                FirstOrDefault(x => x.Id == model.Category.Id);

            var size = context.ProductSize.
                FirstOrDefault(x => x.Id == model.Size.Id);

            context.Entry(size).State = EntityState.Detached;
            context.Entry(category).State = EntityState.Detached;

            context.Product.Add(product);

            context.Entry(context.ProductSize.FirstOrDefault(x => x.Id == product.Size.Id)).State = EntityState.Modified;
            context.Entry(context.ProductCategory.FirstOrDefault(x => x.Id == product.Category.Id)).State = EntityState.Modified;

            return product;
        }
        public Product UpdateProduct(ProductModel model)
        {
            var product = context.Product.FirstOrDefault(x => x.Id == model.Id);
            if (product == null)
                throw new InvalidOperationException();

            product = model.ToEntity(product);
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
