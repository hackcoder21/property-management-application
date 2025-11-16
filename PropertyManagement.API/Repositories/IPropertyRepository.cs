using PropertyManagement.API.Models.Domain;

namespace PropertyManagement.API.Repositories
{
    public interface IPropertyRepository
    {
        Task<List<Property>> GetAllPropertiesAsync();
        Task<Property?> GetPropertyByIdAsync(Guid id);
        Task<Property> CreatePropertyAsync(Property property);
        Task<Property?> UpdatePropertyAsync(Guid id, Property property);
        Task<Property?> DeletePropertyAsync(Guid id);
    }
}