using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PropertyManagement.API.Data;
using PropertyManagement.API.Models.Domain;

namespace PropertyManagement.API.Controllers
{
    // https://localhost:1234/api/property
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        private readonly PMDbContext dbContext;

        public PropertyController(PMDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAllProperties()
        {
            var properties = dbContext.Properties.ToList();
            return Ok(properties);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetPropertyById([FromRoute] Guid id)
        {
            var property = dbContext.Properties.FirstOrDefault(x => x.Id == id);

            if (property == null)
            {
                return NotFound();
            }

            return Ok(property);
        }
    }
}
