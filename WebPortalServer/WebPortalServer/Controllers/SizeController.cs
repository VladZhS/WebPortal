using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebPortalServer.Model.WebEnities;
using WebPortalServer.Services;

namespace WebPortalServer.Controllers
{
    [ApiController]
    [Route("api/sizes")]
    public class SizeController : Controller
    {
        private readonly WebPortalDBContext context;
        private readonly ISizeService service;

        public SizeController(WebPortalDBContext context, ISizeService service)
        {
            this.context = context;
            this.service = service;
        }
        

        [HttpGet]
        public async Task<IEnumerable<SizeModel>> Get()
        {
            return service.ConvertList(await context.ProductSize.ToListAsync());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<SizeModel>> Get(int id)
        {
            try
            {
                var size = await context.ProductSize.FirstOrDefaultAsync(x => x.Id == id);
                return Ok(new SizeModel(size));
            }
            catch
            {
                return BadRequest(new ModelError("id", "Invalid id"));
            }
        } 
    }
}
