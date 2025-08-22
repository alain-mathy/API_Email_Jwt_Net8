using API_Email_Jwt_Net8.Implementations.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API_Email_Jwt_Net8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register/{email}/{password}")]
        public async Task<ActionResult<IdentityUser>> Register(RegisterRequest request)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest("Email and password are required.");
            }

            var user = await _userService.Register(request);
            if (user == null)
            {
                return BadRequest("User registration failed.");
            }

            return Ok(user);
        }
    }
}
