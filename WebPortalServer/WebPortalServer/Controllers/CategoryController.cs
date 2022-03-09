using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebPortalServer.Model.WebEnities;
using WebPortalServer.Services;
using WebPortalServer.Services.Validators;

namespace WebPortalServer.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoryController : Controller
    {
        private readonly WebPortalDBContext context;
        private readonly ICategoryService service;
        private readonly ICategoryValidator validator;

        public CategoryController(WebPortalDBContext context, ICategoryService service, ICategoryValidator validator)
        {
            this.context = context;
            this.service = service;
            this.validator = validator;
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

        [HttpPost]
        public ActionResult<CategoryModel> Post(CategoryModel model)
        {
            ModelError error = validator.IsValid(model);
            if (error.Count > 0)
                return BadRequest(error);

            var category = service.CreateCategory(model);
            return Accepted(new CategoryModel(category));
        }

        [HttpPut]
        public ActionResult<CategoryModel> Put(CategoryModel model)
        {
            ModelError error = validator.IsValid(model);
            if (error.Count > 0)
                return BadRequest(error);
            
            try
            { 
                var category = service.UpdateCategory(model);
                return Accepted(new CategoryModel(category));
            }
            catch
            {
                error.AddError("id", "Invalid id");
                return BadRequest(error);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<CategoryModel> Delete(int id)
        {
            try
            {
                var category = service.DeleteCategory(id);
                return Accepted(new CategoryModel(category));
            }
            catch
            {
                return BadRequest(new ModelError("id", "Invalid id"));
            }
        }
    }
}