using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebPortalServer.Model.WebEnities;
using WebPortalServer.Services;

namespace WebPortalServer.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoryController : Controller
    {
        private readonly WebPortalDBContext context;
        private readonly ICategoryService service;

        public CategoryController(WebPortalDBContext context, ICategoryService service)
        {
            this.context = context;
            this.service = service;
        }

        [HttpGet]
        public async Task<IEnumerable<CategoryModel>> Get()
        {
            return service.ConvertList(await context.ProductCategory.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryModel>> Get(int id)
        {
            try
            {
                var category = new CategoryModel(await context.ProductCategory.FirstOrDefaultAsync(x => x.Id == id));
                return Ok(category);
            }
            catch
            {
                return BadRequest(new ModelError("id", "Invalid id"));
            }
        }
    }
}
