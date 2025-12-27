using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PropertyManagement.API.Models.DTO;
using PropertyManagement.API.Repositories;

namespace PropertyManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }

        // Register User
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDto.Username,
                Email = registerRequestDto.Username
            };

            var identityResult = await userManager.CreateAsync(identityUser, registerRequestDto.Password);

            if (identityResult.Succeeded)
            {
                identityResult = await userManager.AddToRolesAsync(identityUser, new[] { "Writer" });

                if (identityResult.Succeeded)
                {
                    return Ok("User registered successfully");
                }
            }

            return BadRequest("User registration failed");
        }

        // Login User
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            // Get identity user by email
            var user = await userManager.FindByEmailAsync(loginRequestDto.Username);

            if (user != null)
            {
                var checkPasswordResult = await userManager.CheckPasswordAsync(user, loginRequestDto.Password);

                if (checkPasswordResult)
                {
                    // Get user roles
                    var roles = await userManager.GetRolesAsync(user);

                    if (roles != null)
                    {
                        // Create token
                        var token = tokenRepository.CreateToken(user, roles.ToList());

                        var response = new LoginResponseDto
                        {
                            Token = token
                        };

                        return Ok(response);
                    }
                }
            }

            return Unauthorized("Invalid username or password");
        }
    }
}
