using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PropertyManagement.API.Data;
using PropertyManagement.API.Models.Domain;
using PropertyManagement.API.Models.DTO;
using PropertyManagement.API.Repositories;

namespace PropertyManagement.API.Controllers
{
    // https://localhost:1234/api/property
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        private readonly IPropertyRepository propertyRepository;
        private readonly IMapper mapper;

        public PropertyController(IPropertyRepository propertyRepository, IMapper mapper)
        {
            this.propertyRepository = propertyRepository;
            this.mapper = mapper;
        }

        // Get All Properties
        [HttpGet]
        public async Task<IActionResult> GetAllProperties()
        {
            // Get data from database
            var propertiesDomain = await propertyRepository.GetAllPropertiesAsync();

            // Map domain model to DTO
            return Ok(mapper.Map<List<PropertyDto>>(propertiesDomain));
        }

        // Get Property By Id
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetPropertyById([FromRoute] Guid id)
        {
            // Get data from database
            var propertyDomain = await propertyRepository.GetPropertyByIdAsync(id);

            if (propertyDomain == null)
            {
                return NotFound("Property not found");
            }

            // Map domain model to Dto
            return Ok(mapper.Map<PropertyDto>(propertyDomain));
        }

        // Create Property
        [HttpPost]
        public async Task<IActionResult> CreateProperty([FromBody] AddPropertyRequestDto addPropertyRequestDto)
        {
            // Map Dto to domain model
            var propertyDomain = mapper.Map<Property>(addPropertyRequestDto);

            // Create property and save to database
            propertyDomain = await propertyRepository.CreatePropertyAsync(propertyDomain);

            // Map domain model to Dto
            var propertyDto = mapper.Map<PropertyDto>(propertyDomain);

            return CreatedAtAction(nameof(GetPropertyById), new { id = propertyDto.Id }, propertyDto);
        }

        // Update Property
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateProperty([FromRoute] Guid id, [FromBody] UpdatePropertyRequestDto updatePropertyRequestDto)
        {
            // Map Dto to domain model
            var propertyDomain = mapper.Map<Property>(updatePropertyRequestDto);

            // Check if property exists
            propertyDomain = await propertyRepository.UpdatePropertyAsync(id, propertyDomain);

            if (propertyDomain == null)
            {
                return NotFound("Property not found");
            }

            // Map domain model to Dto
            return Ok(mapper.Map<PropertyDto>(propertyDomain));
        }

        // Delete Property
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteProperty([FromRoute] Guid id)
        {
            // Check if property exists
            var propertyDomain = await propertyRepository.DeletePropertyAsync(id);

            if (propertyDomain == null)
            {
                return NotFound("Property not found");
            }

            // Return deleted property (Map domain to Dto)
            return Ok(mapper.Map<PropertyDto>(propertyDomain));
        }
    }
}