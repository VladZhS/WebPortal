using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebPortalServer.Model.WebEnities;
using WebPortalServer.Services;

namespace WebPortalServer.Controllers
{
    [ApiController]
    [Route("api/statuses")]
    public class StatusController : Controller
    {
        private readonly WebPortalDBContext context;
        private readonly IStatusService service;

        public StatusController(WebPortalDBContext context, IStatusService service)
        {
            this.context = context;
            this.service = service;
        }

        [HttpGet]
        public async Task<IEnumerable<StatusModel>> Get()
        {
            return service.ConvertList(await context.OrderStatus.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StatusModel>> Get(int id)
        {
            try
            {
                var status = await context.OrderStatus.FirstOrDefaultAsync(x => x.Id == id);
                return Ok(new StatusModel(status));
            }
            catch
            {
                return BadRequest(new ModelError("id", "Invalid id"));
            }
        }
    }
}
