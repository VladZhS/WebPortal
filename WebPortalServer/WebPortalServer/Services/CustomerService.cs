using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebPortalServer.Model.WebEnities;

namespace WebPortalServer.Services
{
    public class CustomerService : ICustomerService
    {
        private WebPortalDBContext context;
        public CustomerService(WebPortalDBContext context)
        {
            this.context = context;
        }

        public Customer CreateCustomer(CustomerModel model)
        {
            var customer = context.Customer.FirstOrDefault(x => x.Id == model.Id);
            if (customer != null)
                throw new InvalidOperationException();

            customer = model.ToEntity(new Customer());

            context.Customer.Add(customer);
            context.SaveChanges();

            return customer;
        }

        public Customer UpdateCustomer(CustomerModel model)
        {
            var customer = context.Customer.FirstOrDefault(x => x.Id == model.Id);
            if (customer == null)
                throw new InvalidOperationException();

            customer = model.ToEntity(customer);

            context.Update(customer);
            context.SaveChanges();

            return customer;
        }

        public Customer DeleteCustomer(int id)
        {
            var customer = context.Customer.FirstOrDefault(x => x.Id == id);
            if (customer == null)
                throw new InvalidOperationException();

            context.Customer.Remove(customer);
            context.SaveChanges();

            return customer;
        }

        public IEnumerable<CustomerModel> ConvertList(IEnumerable<Customer> collection)
        {
            return collection.ConvertModel<Customer, CustomerModel>();
        }
    }
}