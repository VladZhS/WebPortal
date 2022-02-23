using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebPortalServer.Model.WebEnities;
using WebPortalServer.Services;
using WebPortalServer.Services.Validators;

namespace WebPortalServer.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : Controller
    {
        private readonly WebPortalDBContext context;
        private readonly IProductService service;
        private readonly IProductValidator validator;

        public ProductsController(WebPortalDBContext context, IProductService service, IProductValidator validator)
        {
            this.context = context;
            this.service = service;
            this.validator = validator;
        }

        [HttpGet]
        public async Task<IEnumerable<ProductModel>> Get()
        {
            return service.ConverList(await context.Product.ToListAsync()).ToList();
        }

        [HttpGet("{id}")]
        public async Task<ProductModel> Get(int id)
        {
            return new ProductModel(await context.Product.FirstOrDefaultAsync(x => x.Id == id));
        }

        [HttpPost]
        public ActionResult Post(ProductModel model)
        {
            ModelError error = validator.IsValid(model);
            if (error.Count > 0)
                return BadRequest(error);

            var customer = service.CreateProduct(model);
            return Accepted(new ProductModel(customer));
        }

        [HttpPut]
        public ActionResult<ProductModel> Put(ProductModel model)
        {
            ModelError error = validator.IsValid(model);
            if (error.Count > 0)
                return BadRequest(error);

            var customer = service.UpdateProduct(model);
            return Accepted(new ProductModel(customer));
        }

        [HttpDelete]
        public ActionResult<ProductModel> Delete(int id)
        {
            try
            {
                return Accepted(new ProductModel(service.DeleteProduct(id)));
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
