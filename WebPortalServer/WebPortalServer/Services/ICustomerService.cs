using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebPortalServer.Model.WebEnities;

namespace WebPortalServer.Services
{
    public interface ICustomerService
    {
        public Customer CreateCustomer(CustomerModel model);
        public Customer UpdateCustomer(CustomerModel model);
        public Customer DeleteCustomer(int id);
        public IEnumerable<CustomerModel> ConvertList(IEnumerable<Customer> collection);
    }
}
