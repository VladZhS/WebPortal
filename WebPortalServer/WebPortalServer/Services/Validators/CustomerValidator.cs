using WebPortalServer.Model.WebEnities;

namespace WebPortalServer.Services.Validators
{
    public class CustomerValidator : ICustomerValidator
    {
        public ModelError IsValid(CustomerModel customer)
        {
            ModelError error = new ModelError();
            if (customer == null)
                error.AddError("customer", "Customer can't be null");
            else
            {
                if (customer.Name == null)
                    error.AddError("name", "Name can't be null");
                if (customer.Name == "")
                    error.AddError("name", "Name can't be empty");
                if (customer.Address == null)
                    error.AddError("name", "Address can't be null");
                if (customer.Address == "")
                    error.AddError("name", "Address can't be empty");
            }

            return error;
        }
    }
}
