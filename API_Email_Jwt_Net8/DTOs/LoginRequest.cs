using System.ComponentModel.DataAnnotations;

namespace API_Email_Jwt_Net8.DTOs
{
    public class LoginRequest
    {
        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}
