using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WebPortalServer.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : Controller
    {
        private WebPortalDBContext context;

        public ProductsController(WebPortalDBContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IEnumerable<Product> Get() => context.Product.AsQueryable();
    }
}
