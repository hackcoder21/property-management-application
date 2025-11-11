using Microsoft.EntityFrameworkCore;
using PropertyManagement.API.Models.Domain;

namespace PropertyManagement.API.Data
{
    public class PMDbContext: DbContext
    {
        public PMDbContext(DbContextOptions dbContextOptions): base(dbContextOptions) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Property> Properties { get; set; }
    }
}
