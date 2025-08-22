using API_Email_Jwt_Net8.DTOs;
using API_Email_Jwt_Net8.Implementations.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace API_Email_Jwt_Net8.Implementations.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IEmailService _emailService;

        public UserService(UserManager<IdentityUser> userManager, IEmailService emailService)
        {
            _userManager = userManager;
            _emailService = emailService;
        }
        public async Task<IdentityUser> GetUserByEmail(string email)
        {
            var result = await _userManager.FindByEmailAsync(email);
            return result;
        }
        public async Task<IdentityUser> Register(RegisterRequest request)
        {
            var user = await GetUserByEmail(request.Email);
            if (user is not null)
            {
                return null;
            }

            var result = await _userManager.CreateAsync(new IdentityUser()
            {
                UserName = request.Email,
                Email = request.Email,
                PasswordHash = request.Password
            }, request.Password);

            if (!result.Succeeded)
            {
                return null;
            }

            var createdUser = await GetUserByEmail(request.Email);
            var emailCode = await _userManager.GenerateEmailConfirmationTokenAsync(createdUser);
            string sendEmail = await _emailService.SendEmail(request.Email, emailCode);
            return createdUser;
        }
    }
}
