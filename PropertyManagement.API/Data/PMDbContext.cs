using Microsoft.EntityFrameworkCore;
using PropertyManagement.API.Models.Domain;

namespace PropertyManagement.API.Data
{
    public class PMDbContext: DbContext
    {
        public PMDbContext(DbContextOptions dbContextOptions): base(dbContextOptions) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Property> Properties { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed data for User
            var user = new User()
            {
                Id = Guid.NewGuid(),
                FullName = "user",
                Email = "user@user.com",
            };

            modelBuilder.Entity<User>().HasData(user);
        }
    }
}
