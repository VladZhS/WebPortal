using WebPortalServer.Model.WebEnities;

namespace WebPortalServer.Services.Validators
{
    public interface IProductValidator
    {
        public ModelError IsValid(ProductModel product);

    }
}
