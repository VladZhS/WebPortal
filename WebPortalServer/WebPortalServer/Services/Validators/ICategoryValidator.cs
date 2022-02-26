
using WebPortalServer.Model.WebEnities;

namespace WebPortalServer.Services.Validators
{
    public interface ICategoryValidator
    {
        public ModelError IsValid(CategoryModel category);
    }
}
