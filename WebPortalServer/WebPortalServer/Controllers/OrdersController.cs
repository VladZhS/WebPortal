using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;

namespace WebPortalServer.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : Controller
    {
        private WebPortalDBContext context;

        public OrdersController(WebPortalDBContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IEnumerable<Order> Get() => context.Order.AsQueryable();



    }
}
