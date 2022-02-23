using System.Collections;
using System.Collections.Generic;
using WebPortalServer.Model.WebEnities;

namespace WebPortalServer.Services
{
    public interface IProductService
    {
        public Product CreateProduct(ProductModel model);
        public Product UpdateProduct(ProductModel model);
        public Product DeleteProduct(int id);
        public IEnumerable<ProductModel> ConverList(IEnumerable<Product> collection);
    }
}
