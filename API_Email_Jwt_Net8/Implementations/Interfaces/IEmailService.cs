namespace API_Email_Jwt_Net8.Implementations.Interfaces
{
    public interface IEmailService
    {
        Task<string> SendEmail(string email, string emailCode);
    }
}
