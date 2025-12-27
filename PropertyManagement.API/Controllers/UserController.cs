using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PropertyManagement.API.CustomActionFilters;
using PropertyManagement.API.Models.Domain;
using PropertyManagement.API.Models.DTO;
using PropertyManagement.API.Repositories;

namespace PropertyManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        // Create User
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreateUser([FromBody] AddUserRequestDto addUserRequestDto)
        {
            // Map Dto to domain model
            var userDomain = mapper.Map<User>(addUserRequestDto);

            // Create user and save to database
            userDomain = await userRepository.CreateUserAsync(userDomain);

            // Return user
            return Ok(mapper.Map<UserDto>(userDomain));
        }

        // Get All Users
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            // Get data from database
            var usersDomain = await userRepository.GetAllUsersAsync();

            // Return users
            return Ok(mapper.Map<List<UserDetailsDto>>(usersDomain));
        }

        // Get User By Id
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            // Get user from database
            var userDomain = await userRepository.GetUserByIdAsync(id);

            if (userDomain == null)
            {
                return NotFound("User not found");
            }

            // Return user
            return Ok(mapper.Map<UserDetailsDto>(userDomain));
        }

        // Delete User
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            // Check if user exists
            var userDomain = await userRepository.DeleteUserAsync(id);

            if (userDomain == null)
            {
                return NotFound("User not found");
            }

            // Return deleted user
            return Ok(mapper.Map<UserDto>(userDomain));
        }
    }
}