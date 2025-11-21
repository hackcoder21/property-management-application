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

        public async Task<List<Property>> GetAllPropertiesAsync(
            string? filterOn = null, 
            string? filterQuery = null, 
            string? sortBy = null, 
            bool isAscending = true,
            int pageNumber = 1,
            int pageSize = 10)
        {
            var properties = dbContext.Properties.Include(u => u.User).AsQueryable();

            // Filtering
            if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
            {
                switch (filterOn.ToLower())
                {
                    case "title":
                        properties = properties.Where(p => p.Title.Contains(filterQuery));
                        break;
                    case "city":
                        properties = properties.Where(p => p.City.Contains(filterQuery));
                        break;
                    case "state":
                        properties = properties.Where(p => p.State.Contains(filterQuery));
                        break;
                    case "locality":
                        properties = properties.Where(p => p.Locality.Contains(filterQuery));
                        break;
                    case "pincode":
                        properties = properties.Where(p => p.Pincode.Contains(filterQuery));
                        break;
                    case "noofrooms":
                        if (int.TryParse(filterQuery, out var roomsValue))
                            properties = properties.Where(p => p.NoOfRooms == roomsValue);
                        break;
                    case "builtyear":
                        if (int.TryParse(filterQuery, out var yearValue))
                            properties = properties.Where(p => p.BuiltYear == yearValue);
                        break;
                    case "balcony":
                        if (bool.TryParse(filterQuery, out bool balconyValue))
                            properties = properties.Where(p => p.Balcony == balconyValue);
                        break;
                    case "parking":
                        if (bool.TryParse(filterQuery, out bool parkingValue))
                            properties = properties.Where(p => p.Parking == parkingValue);
                        break;
                }
            }

            // Sorting
            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                switch (sortBy.ToLower())
                {
                    case "title":
                        properties = isAscending ? properties.OrderBy(p => p.Title) : properties.OrderByDescending(p => p.Title);
                        break;
                    case "city":
                        properties = isAscending ? properties.OrderBy(p => p.City) : properties.OrderByDescending(p => p.City);
                        break;
                    case "state":
                        properties = isAscending ? properties.OrderBy(p => p.State) : properties.OrderByDescending(p => p.State);
                        break;
                    case "locality":
                        properties = isAscending ? properties.OrderBy(p => p.Locality) : properties.OrderByDescending(p => p.Locality);
                        break;
                    case "noofrooms":
                        properties = isAscending ? properties.OrderBy(p => p.NoOfRooms) : properties.OrderByDescending(p => p.NoOfRooms);
                        break;
                    case "builtyear":
                        properties = isAscending ? properties.OrderBy(p => p.BuiltYear) : properties.OrderByDescending(p => p.BuiltYear);
                        break;
                    case "price":
                        properties = isAscending ? properties.OrderBy(p => p.Price) : properties.OrderByDescending(p => p.Price);
                        break;
                }
            }

            // Pagination
            pageNumber = pageNumber <= 0 ? 1 : pageNumber;
            pageSize = pageSize <= 0 ? 10 : pageSize;

            int skipResults = (pageNumber - 1) * pageSize;

            return await properties.Skip(skipResults).Take(pageSize).ToListAsync();
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