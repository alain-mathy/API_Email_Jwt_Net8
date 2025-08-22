using API_Email_Jwt_Net8.DTOs;
using Microsoft.AspNetCore.Identity;

namespace API_Email_Jwt_Net8.Implementations.Interfaces
{
    public interface IUserService
    {
        Task<IdentityUser> GetUserByEmail(string email);
        Task<IdentityUser> Register(RegisterRequest request);
        Task<string> Confirmation(string email, int code);
    }
}
