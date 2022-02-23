using WebPortalServer.Model.WebEnities;

namespace WebPortalServer.Services.Validators
{
    public interface ICustomerValidator
    {
        public ModelError IsValid(CustomerModel customer);
    }
}
