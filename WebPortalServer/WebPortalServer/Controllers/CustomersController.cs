﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebPortalServer.Model.WebEnities;
using WebPortalServer.Services;
using WebPortalServer.Services.Validators;

namespace WebPortalServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : Controller
    {
        private readonly WebPortalDBContext context;
        private readonly ICustomerService service;
        private readonly ICustomerValidator validator;

        public CustomersController(WebPortalDBContext context, ICustomerService service, ICustomerValidator validator)
        {
            this.context = context;
            this.service = service;
            this.validator = validator;
        }

        [HttpGet]
        public async Task<IEnumerable<CustomerModel>> Get()
        {
            return service.ConvertList(await context.Customer.ToListAsync()).ToList();
        }

        [HttpGet("{id}")]
        public async Task<CustomerModel> Get(int id)
        {
            return new CustomerModel(await context.Customer.FirstOrDefaultAsync(x => x.Id == id));
        }

        [HttpPost]
        public IActionResult Post(CustomerModel model)
        {
            var error = validator.IsValid(model);
            if (error.Count != 0)
                return BadRequest(error);

            var customer = service.CreateCustomer(model);
            return Accepted(new CustomerModel(customer));
        }

        [HttpPut]
        public IActionResult Put(CustomerModel model)
        {
            var error = validator.IsValid(model);
            if (error.Count != 0)
                return BadRequest(error);

            try
            {
                service.UpdateCustomer(model);
                return Accepted(model);
            }
            catch
            {
                error.AddError("id", "Invalid id");
                return BadRequest(error);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                return Accepted(new CustomerModel(service.DeleteCustomer(id)));
            }
            catch
            {
                var error = new ModelError();
                error.AddError("id", "Invalid id");
                return BadRequest(error);
            }
        }
    }
}
