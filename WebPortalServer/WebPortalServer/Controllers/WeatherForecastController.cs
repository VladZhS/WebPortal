using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebPortalServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private WebPortalDBContext context;

        public WeatherForecastController(WebPortalDBContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            
            Customer customer = new Customer()
            {
                Name = "Mykhayl Berlios",
                Address = "Bolshaya Sadovaya 302"
            };

            context.Customer.Add(customer);

            context.SaveChanges();
            
            return Ok();
            /*
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();*/
        }
    }
}
