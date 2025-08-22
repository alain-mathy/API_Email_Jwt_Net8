using API_Email_Jwt_Net8.DTOs;
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

        [HttpPost("confirmation/{email}/{code:int}")]
        public async Task<ActionResult<string>> Confirmation(string email, int code)
        {
            var result = await _userService.Confirmation(email, code);
            if (result != "Email confirmed successfully")
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
