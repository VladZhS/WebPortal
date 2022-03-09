using System;
using System.Collections.Generic;

namespace WebPortalServer.Model.WebEnities
{
    public class CustomerModel : BaseModel<Customer>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string CreationDate { get; set; }

        public CustomerModel() { }
        public CustomerModel(Customer customer)
        {
            if (customer == null)
                throw new ArgumentNullException();
            Id = customer.Id;
            Name = customer.Name;
            Address = customer.Address;
            CreationDate = customer.CreationDate.Date.ToString();
        }

        public override Customer ToEntity(Customer customer)
        {
            customer.Id = Id;
            customer.Name = Name;
            customer.Address = Address;
            DateTime date = new DateTime();
            DateTime.TryParse(CreationDate, out date);
            customer.CreationDate = date;
            return customer;
        }
    }
}
