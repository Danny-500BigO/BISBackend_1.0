using BakeryApi.Application.Services.Users;
using BakeryApi.Domain.Entities;
using BakeryApi.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace BakeryApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;
        private readonly BakeryDbContext _context;

        public UserController(
            IUserService userService,
            BakeryDbContext context,
            ILogger<UserController> logger
        )
        {
            _userService = userService;
            _context = context;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        //post api/user
        [HttpPost]
        public async Task<ActionResult<User>> PostUser([FromBody] User user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest("Bad Request!");
                }

                _logger.LogInformation("Creating user...");

                var createdUser = await _userService.AddUserAsync(user);
                var response = new userCreatedResponse
                {
                    StatusCode = 201,
                    Username = createdUser.user_name,
                    IsActive = createdUser.is_active,
                    ResponseMessage = "Successfully Created",
                };

                return CreatedAtAction(nameof(GetUser), new { id = createdUser.user_id }, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching item");
                return StatusCode(500, "Internal Server Error");
            }
        }

        //get by id
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);

            if (user == null)
            {
                Console.WriteLine($"User with ID {id} not found in database/service.");
                return NotFound();
            }
            return Ok(user);
        }

        //user Login
        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseDto>> UserLogin([FromBody] LoginRequest request)
        {
            try
            {
                if (request == null)
                {
                    return BadRequest("User Name and Password Required");
                }

                var loginUser = await _userService.LoginAsync(request);
                var response = new userCreatedResponse
                {
                    StatusCode = 200,
                    Username = loginUser.user_name,
                    ResponseMessage = "Login Succesfull",
                };

                return loginUser;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error fetching item");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
