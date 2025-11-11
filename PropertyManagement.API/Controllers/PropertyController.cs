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
        public IActionResult CreateProperty([FromBody] AddPropertyRequestDto addPropertyRequestDto)
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
            dbContext.Properties.Add(propertyDomain);
            dbContext.SaveChanges();

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
        public IActionResult UpdateProperty([FromRoute] Guid id, [FromBody] UpdatePropertyRequestDto updatePropertyRequestDto)
        {
            // Check if property exists
            var propertyDomain = dbContext.Properties.FirstOrDefault(x => x.Id == id);

            if (propertyDomain == null)
            {
                return NotFound("Property not found");
            }

            // Update domain model
            propertyDomain.Title = updatePropertyRequestDto.Title;
            propertyDomain.Price = updatePropertyRequestDto.Price;
            propertyDomain.City = updatePropertyRequestDto.City;
            propertyDomain.State = updatePropertyRequestDto.State;
            propertyDomain.Locality = updatePropertyRequestDto.Locality;
            propertyDomain.Pincode = updatePropertyRequestDto.Pincode;
            propertyDomain.NoOfRooms = updatePropertyRequestDto.NoOfRooms;
            propertyDomain.CarpetAreaSqft = updatePropertyRequestDto.CarpetAreaSqft;
            propertyDomain.BuiltYear = updatePropertyRequestDto.BuiltYear;
            propertyDomain.Balcony = updatePropertyRequestDto.Balcony;
            propertyDomain.Parking = updatePropertyRequestDto.Parking;
            propertyDomain.PropertyImageUrl = updatePropertyRequestDto.PropertyImageUrl;
            propertyDomain.HallImageUrl = updatePropertyRequestDto.HallImageUrl;
            propertyDomain.KitchenImageUrl = updatePropertyRequestDto.KitchenImageUrl;
            propertyDomain.BathroomImageUrl = updatePropertyRequestDto.BathroomImageUrl;
            propertyDomain.BedroomImageUrl = updatePropertyRequestDto.BedroomImageUrl;
            propertyDomain.ParkingImageUrl = updatePropertyRequestDto.ParkingImageUrl;

            dbContext.SaveChanges();

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
    }
}
