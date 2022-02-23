using System.Collections.Generic;
using WebPortalServer.Model.WebEnities;

namespace WebPortalServer.Services
{
    public interface ICategoryService
    {
        public ProductCategory CreateCategory(CategoryModel model);
        public ProductCategory UpdateCategory(CategoryModel model);
        public ProductCategory DeleteCategory(int id);
        public IEnumerable<CategoryModel> ConvertList(IEnumerable<ProductCategory> collection);
    }
}
