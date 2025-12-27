using Microsoft.AspNetCore.Identity;

namespace PropertyManagement.API.Repositories
{
    public interface ITokenRepository
    {
        string CreateToken(IdentityUser user, List<string> roles);
    }
}
