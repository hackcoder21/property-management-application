using PropertyManagement.API.Models.Domain;

namespace PropertyManagement.API.Repositories
{
    public interface IUserRepository
    {
        Task<User> CreateUserAsync(User user);
        Task<List<User>> GetAllUsersAsync();
        Task<User?> GetUserByIdAsync(Guid id);
        Task<User?> DeleteUserAsync(Guid id);
    }
}