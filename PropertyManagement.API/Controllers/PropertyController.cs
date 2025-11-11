using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PropertyManagement.API.Data;
using PropertyManagement.API.Models.Domain;
using PropertyManagement.API.Models.DTO;

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
            // Get data from database
            var propertiesDomain = dbContext.Properties.ToList();

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
        public IActionResult GetPropertyById([FromRoute] Guid id)
        {
            // Get data from database
            var propertyDomain = dbContext.Properties.FirstOrDefault(x => x.Id == id);

            if (propertyDomain == null)
            {
                return NotFound();
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
    }
}
