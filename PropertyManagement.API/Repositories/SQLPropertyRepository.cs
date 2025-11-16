using Microsoft.EntityFrameworkCore;
using PropertyManagement.API.Data;
using PropertyManagement.API.Models.Domain;

namespace PropertyManagement.API.Repositories
{
    public class SQLPropertyRepository : IPropertyRepository
    {
        private readonly PMDbContext dbContext;

        public SQLPropertyRepository(PMDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Property> CreatePropertyAsync(Property property)
        {
            await dbContext.Properties.AddAsync(property);
            await dbContext.SaveChangesAsync();
            return property;
        }

        public async Task<Property?> DeletePropertyAsync(Guid id)
        {
            var existingProperty = await dbContext.Properties.FirstOrDefaultAsync(x => x.Id == id);

            if (existingProperty == null)
            {
                return null;
            }

            dbContext.Properties.Remove(existingProperty);
            await dbContext.SaveChangesAsync();
            return existingProperty;
        }

        public async Task<List<Property>> GetAllPropertiesAsync()
        {
            return await dbContext.Properties.Include("User").ToListAsync();
        }

        public async Task<Property?> GetPropertyByIdAsync(Guid id)
        {
            return await dbContext.Properties.Include("User").FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Property?> UpdatePropertyAsync(Guid id, Property property)
        {
            var existingProperty = await dbContext.Properties.FirstOrDefaultAsync(x => x.Id == id);

            if (existingProperty == null)
            {
                return null;
            }

            existingProperty.Title = property.Title;
            existingProperty.Price = property.Price;
            existingProperty.City = property.City;
            existingProperty.State = property.State;
            existingProperty.Locality = property.Locality;
            existingProperty.Pincode = property.Pincode;
            existingProperty.NoOfRooms = property.NoOfRooms;
            existingProperty.CarpetAreaSqft = property.CarpetAreaSqft;
            existingProperty.BuiltYear = property.BuiltYear;
            existingProperty.Balcony = property.Balcony;
            existingProperty.Parking = property.Parking;
            existingProperty.PropertyImageUrl = property.PropertyImageUrl;
            existingProperty.HallImageUrl = property.HallImageUrl;
            existingProperty.KitchenImageUrl = property.KitchenImageUrl;
            existingProperty.BathroomImageUrl = property.BathroomImageUrl;
            existingProperty.BedroomImageUrl = property.BedroomImageUrl;
            existingProperty.ParkingImageUrl = property.ParkingImageUrl;

            await dbContext.SaveChangesAsync();
            return existingProperty;
        }
    }
}