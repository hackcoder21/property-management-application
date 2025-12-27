using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PropertyManagement.API.Data
{
    public class PMAuthDbContext : IdentityDbContext
    {
        public PMAuthDbContext(DbContextOptions<PMAuthDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleId = "1555aeb5-9fb0-4739-b63e-9ba33f16ef76";
            var writerRoleId = "a00d71e5-90dd-4be1-ba25-5d43b111344d";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = readerRoleId,
                    ConcurrencyStamp = readerRoleId,
                    Name = "Reader",
                    NormalizedName = "READER"
                },
                new IdentityRole
                {
                    Id = writerRoleId,
                    ConcurrencyStamp = writerRoleId,
                    Name = "Writer",
                    NormalizedName = "WRITER"
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
