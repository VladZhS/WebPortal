using System;
using System.Collections.Generic;
using System.Linq;
using WebPortalServer.Model.WebEnities;

namespace WebPortalServer.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly WebPortalDBContext context;

        public CategoryService(WebPortalDBContext context)
        {
            this.context = context;
        }

        public ProductCategory CreateCategory(CategoryModel model)
        {
            var category = model.ToEntity(new ProductCategory());
            context.ProductCategory.Add(category);
            context.SaveChanges();

            return category;
        }

        public ProductCategory UpdateCategory(CategoryModel model)
        {
            var category = context.ProductCategory.FirstOrDefault(x => x.Id == model.Id);
            if (category == null)
                throw new InvalidOperationException();

            category = model.ToEntity(category);
            context.ProductCategory.Update(category);
            context.SaveChanges();

            return category;
        }
        public ProductCategory DeleteCategory(int id)
        {
            var category = context.ProductCategory.FirstOrDefault(x => x.Id == id);
            if (category == null)
                throw new InvalidOperationException();

            context.ProductCategory.Remove(category);
            context.SaveChanges();

            return category;
        }
        public IEnumerable<CategoryModel> ConvertList(IEnumerable<ProductCategory> collection)
        {
            return collection.ConvertModel<ProductCategory, CategoryModel>();
        }
    }
}
