using WebPortalServer.Model.WebEnities;

namespace WebPortalServer.Services.Validators
{
    public class CategoryValidator : ICategoryValidator
    {
        public ModelError IsValid(CategoryModel category)
        {
            ModelError error = new ModelError();
            if (category == null)
                error.AddError("category", "Category can't be null");
            else
            {
                if (category.Category == null)
                    error.AddError("categoryName", "Category name can't be null");
                else if (category.Category == "")
                    error.AddError("categoryName", "Category name can't be empty");
            }
            return error;
        }
    }
}
