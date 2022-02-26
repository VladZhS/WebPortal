using System;

namespace WebPortalServer.Model.WebEnities
{
    public class CategoryModel : BaseModel<ProductCategory>
    {
        public int Id { get; set; } 
        public string Category { get; set; }
        public CategoryModel() { }
        public CategoryModel(ProductCategory category)
        {
            if (category == null)
                throw new ArgumentNullException();
            Id = category.Id;
            Category = category.Category;
        }

        public override ProductCategory ToEntity(ProductCategory category)
        {
            category.Id = Id;
            category.Category = Category;
            return category;
        }
    }
}
