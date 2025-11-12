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
        private readonly PMDbContext dbContext;
        private readonly IPropertyRepository propertyRepository;

        public PropertyController(PMDbContext dbContext, IPropertyRepository propertyRepository)
        {
            this.dbContext = dbContext;
            this.propertyRepository = propertyRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProperties()
        {
            // Get data from database
            var propertiesDomain = await propertyRepository.GetAllPropertiesAsync();

            // Map domain model to DTO
            var propertiesDto = new List<PropertyDto>();

            foreach (var propertyDomain in propertiesDomain)
            {
                propertiesDto.Add(new PropertyDto()
                {
                    Id = propertyDomain.Id,
                    UserId = propertyDomain.UserId,
                    User = propertyDomain.User,
                    Title = propertyDomain.Title,
                    Price = propertyDomain.Price,
                    City = propertyDomain.City,
                    State = propertyDomain.State,
                    Locality = propertyDomain.Locality,
                    Pincode = propertyDomain.Pincode,
                    NoOfRooms = propertyDomain.NoOfRooms,
                    CarpetAreaSqft = propertyDomain.CarpetAreaSqft,
                    BuiltYear = propertyDomain.BuiltYear,
                    Balcony = propertyDomain.Balcony,
                    Parking = propertyDomain.Parking,
                    PropertyImageUrl = propertyDomain.PropertyImageUrl,
                    HallImageUrl = propertyDomain.HallImageUrl,
                    KitchenImageUrl = propertyDomain.KitchenImageUrl,
                    BathroomImageUrl = propertyDomain.BathroomImageUrl,
                    BedroomImageUrl = propertyDomain.BedroomImageUrl,
                    ParkingImageUrl = propertyDomain.ParkingImageUrl
                });
            }

            // Return Dto response
            return Ok(propertiesDto);
        }

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
            var propertyDto = new PropertyDto
            {
                Id = propertyDomain.Id,
                UserId = propertyDomain.UserId,
                User = propertyDomain.User,
                Title = propertyDomain.Title,
                Price = propertyDomain.Price,
                City = propertyDomain.City,
                State = propertyDomain.State,
                Locality = propertyDomain.Locality,
                Pincode = propertyDomain.Pincode,
                NoOfRooms = propertyDomain.NoOfRooms,
                CarpetAreaSqft = propertyDomain.CarpetAreaSqft,
                BuiltYear = propertyDomain.BuiltYear,
                Balcony = propertyDomain.Balcony,
                Parking = propertyDomain.Parking,
                PropertyImageUrl = propertyDomain.PropertyImageUrl,
                HallImageUrl = propertyDomain.HallImageUrl,
                KitchenImageUrl = propertyDomain.KitchenImageUrl,
                BathroomImageUrl = propertyDomain.BathroomImageUrl,
                BedroomImageUrl = propertyDomain.BedroomImageUrl,
                ParkingImageUrl = propertyDomain.ParkingImageUrl
            };

            // Return Dto response
            return Ok(propertyDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProperty([FromBody] AddPropertyRequestDto addPropertyRequestDto)
        {
            // Map Dto to domain model
            var propertyDomain = new Property
            {
                UserId = addPropertyRequestDto.UserId,
                Title = addPropertyRequestDto.Title,
                Price = addPropertyRequestDto.Price,
                City = addPropertyRequestDto.City,
                State = addPropertyRequestDto.State,
                Locality = addPropertyRequestDto.Locality,
                Pincode = addPropertyRequestDto.Pincode,
                NoOfRooms = addPropertyRequestDto.NoOfRooms,
                CarpetAreaSqft = addPropertyRequestDto.CarpetAreaSqft,
                BuiltYear = addPropertyRequestDto.BuiltYear,
                Balcony = addPropertyRequestDto.Balcony,
                Parking = addPropertyRequestDto.Parking,
                PropertyImageUrl = addPropertyRequestDto.PropertyImageUrl,
                HallImageUrl = addPropertyRequestDto.HallImageUrl,
                KitchenImageUrl = addPropertyRequestDto.KitchenImageUrl,
                BathroomImageUrl = addPropertyRequestDto.BathroomImageUrl,
                BedroomImageUrl = addPropertyRequestDto.BedroomImageUrl,
                ParkingImageUrl = addPropertyRequestDto.ParkingImageUrl
            };

            // Create property and save to database
            propertyDomain = await propertyRepository.CreatePropertyAsync(propertyDomain);

            // Map domain model to Dto
            var propertyDto = new PropertyDto
            {
                Id = propertyDomain.Id,
                UserId = propertyDomain.UserId,
                User = propertyDomain.User,
                Title = propertyDomain.Title,
                Price = propertyDomain.Price,
                City = propertyDomain.City,
                State = propertyDomain.State,
                Locality = propertyDomain.Locality,
                Pincode = propertyDomain.Pincode,
                NoOfRooms = propertyDomain.NoOfRooms,
                CarpetAreaSqft = propertyDomain.CarpetAreaSqft,
                BuiltYear = propertyDomain.BuiltYear,
                Balcony = propertyDomain.Balcony,
                Parking = propertyDomain.Parking,
                PropertyImageUrl = propertyDomain.PropertyImageUrl,
                HallImageUrl = propertyDomain.HallImageUrl,
                KitchenImageUrl = propertyDomain.KitchenImageUrl,
                BathroomImageUrl = propertyDomain.BathroomImageUrl,
                BedroomImageUrl = propertyDomain.BedroomImageUrl,
                ParkingImageUrl = propertyDomain.ParkingImageUrl
            };

            return CreatedAtAction(nameof(GetPropertyById), new { id = propertyDto.Id }, propertyDto);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateProperty([FromRoute] Guid id, [FromBody] UpdatePropertyRequestDto updatePropertyRequestDto)
        {
            // Map Dto to domain model
            var propertyDomain = new Property
            {
                Title = updatePropertyRequestDto.Title,
                Price = updatePropertyRequestDto.Price,
                City = updatePropertyRequestDto.City,
                State = updatePropertyRequestDto.State,
                Locality = updatePropertyRequestDto.Locality,
                Pincode = updatePropertyRequestDto.Pincode,
                NoOfRooms = updatePropertyRequestDto.NoOfRooms,
                CarpetAreaSqft = updatePropertyRequestDto.CarpetAreaSqft,
                BuiltYear = updatePropertyRequestDto.BuiltYear,
                Balcony = updatePropertyRequestDto.Balcony,
                Parking = updatePropertyRequestDto.Parking,
                PropertyImageUrl = updatePropertyRequestDto.PropertyImageUrl,
                HallImageUrl = updatePropertyRequestDto.HallImageUrl,
                KitchenImageUrl = updatePropertyRequestDto.KitchenImageUrl,
                BathroomImageUrl = updatePropertyRequestDto.BathroomImageUrl,
                BedroomImageUrl = updatePropertyRequestDto.BedroomImageUrl,
                ParkingImageUrl = updatePropertyRequestDto.ParkingImageUrl
            };

            // Check if property exists
            propertyDomain = await propertyRepository.UpdatePropertyAsync(id, propertyDomain);

            if (propertyDomain == null)
            {
                return NotFound("Property not found");
            }

            // Map domain model to Dto
            var propertyDto = new PropertyDto
            {
                Id = propertyDomain.Id,
                UserId = propertyDomain.UserId,
                Title = propertyDomain.Title,
                Price = propertyDomain.Price,
                City = propertyDomain.City,
                State = propertyDomain.State,
                Locality = propertyDomain.Locality,
                Pincode = propertyDomain.Pincode,
                NoOfRooms = propertyDomain.NoOfRooms,
                CarpetAreaSqft = propertyDomain.CarpetAreaSqft,
                BuiltYear = propertyDomain.BuiltYear,
                Balcony = propertyDomain.Balcony,
                Parking = propertyDomain.Parking,
                PropertyImageUrl = propertyDomain.PropertyImageUrl,
                HallImageUrl = propertyDomain.HallImageUrl,
                KitchenImageUrl = propertyDomain.KitchenImageUrl,
                BathroomImageUrl = propertyDomain.BathroomImageUrl,
                BedroomImageUrl = propertyDomain.BedroomImageUrl,
                ParkingImageUrl = propertyDomain.ParkingImageUrl,
                User = propertyDomain.User
            };

            return Ok(propertyDto);
        }

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
            var propertyDto = new PropertyDto
            {
                Id = propertyDomain.Id,
                UserId = propertyDomain.UserId,
                Title = propertyDomain.Title,
                Price = propertyDomain.Price,
                City = propertyDomain.City,
                State = propertyDomain.State,
                Locality = propertyDomain.Locality,
                Pincode = propertyDomain.Pincode,
                NoOfRooms = propertyDomain.NoOfRooms,
                CarpetAreaSqft = propertyDomain.CarpetAreaSqft,
                BuiltYear = propertyDomain.BuiltYear,
                Balcony = propertyDomain.Balcony,
                Parking = propertyDomain.Parking,
                PropertyImageUrl = propertyDomain.PropertyImageUrl,
                HallImageUrl = propertyDomain.HallImageUrl,
                KitchenImageUrl = propertyDomain.KitchenImageUrl,
                BathroomImageUrl = propertyDomain.BathroomImageUrl,
                BedroomImageUrl = propertyDomain.BedroomImageUrl,
                ParkingImageUrl = propertyDomain.ParkingImageUrl,
                User = propertyDomain.User
            };

            return Ok(propertyDto);
        }
    }
}
