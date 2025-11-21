using PropertyManagement.API.Models.Domain;

namespace PropertyManagement.API.Repositories
{
    public interface IPropertyRepository
    {
        Task<List<Property>> GetAllPropertiesAsync(
            string? filterOn = null, 
            string? filterQuery = null, 
            string? sortBy = null, 
            bool isAscending = true,
            int pageNumber = 1,
            int pageSize = 10);
        Task<Property?> GetPropertyByIdAsync(Guid id);
        Task<Property> CreatePropertyAsync(Property property);
        Task<Property?> UpdatePropertyAsync(Guid id, Property property);
        Task<Property?> DeletePropertyAsync(Guid id);
    }
}