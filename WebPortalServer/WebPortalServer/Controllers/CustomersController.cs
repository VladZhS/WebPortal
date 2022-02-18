using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebPortalServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : Controller
    {
        private WebPortalDBContext context;
        public CustomersController(WebPortalDBContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IEnumerable<Customer> Get() => context.Customer.AsQueryable();
    }
}
