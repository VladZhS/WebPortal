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
        public async Task<ActionResult<ProductModel>> Get(int id)
        {
            try
            {
                var product = new ProductModel(await context.Product.FirstOrDefaultAsync(x => x.Id == id));
                return Ok(product);
            }
            catch
            {
                return BadRequest(new ModelError("id", "Invalid id"));
            }
        }

        [HttpPost]
        public ActionResult Post(ProductModel model)
        {
            ModelError error = validator.IsValid(model);
            if (error.Count > 0)
                return BadRequest(error);

            var product = service.CreateProduct(model);
            return Accepted(new ProductModel(product));
        }

        [HttpPut]
        public ActionResult<ProductModel> Put(ProductModel model)
        {
            ModelError error = validator.IsValid(model);
            if (error.Count > 0)
                return BadRequest(error);

            try
            {
                var product = service.UpdateProduct(model);
                return Accepted(new ProductModel(product));
            }
            catch
            {
                error.AddError("id", "Invalid id");
                return BadRequest(error);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<ProductModel> Delete(int id)
        {
            try
            {
                if (context.Order
                    .Include(o => o.OrderProduct)
                    .FirstOrDefault(x => x.OrderProduct
                        .FirstOrDefault(p => p.ProductId == id) != null) 
                    != null)
                    return BadRequest(new ModelError("order", "Can't delete product contained in order"));

                var product = service.DeleteProduct(id);
                return Accepted(new ProductModel(product));
            }
            catch
            {
                return BadRequest(new ModelError("id", "Invalid id"));
            }
        }
    }
}
