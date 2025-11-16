using Microsoft.EntityFrameworkCore;
using PropertyManagement.API.Data;
using PropertyManagement.API.Models.Domain;

namespace PropertyManagement.API.Repositories
{
    public class SQLUserRepository : IUserRepository
    {
        private readonly PMDbContext dbContext;

        public SQLUserRepository(PMDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<User> CreateUserAsync(User user)
        {
            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<User?> DeleteUserAsync(Guid id)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
            {
                return null;
            }

            dbContext.Users.Remove(user);
            await dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await dbContext.Users.Include("Properties").ToListAsync();
        }

        public async Task<User?> GetUserByIdAsync(Guid id)
        {
            return await dbContext.Users.Include("Properties").FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}