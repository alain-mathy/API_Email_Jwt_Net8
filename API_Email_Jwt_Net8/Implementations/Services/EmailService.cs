using API_Email_Jwt_Net8.Implementations.Interfaces;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;
using System.Text;

namespace API_Email_Jwt_Net8.Implementations.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task<string> SendEmail(string email, string emailCode)
        {
            StringBuilder emailMessage = new StringBuilder();
            emailMessage.AppendLine("<html>");
            emailMessage.AppendLine("<body>");
            emailMessage.AppendLine($"<p>Dear {email},</p>");
            emailMessage.AppendLine("<p>Thank you for registering. To verify your email address, please use the following verification code</p>");
            emailMessage.AppendLine($"<h2>Verificaion Code : {emailCode}</h2>");
            emailMessage.AppendLine("<p>Please enter this code on our website to complete your registration</p>");
            emailMessage.AppendLine("<p>If you did not request this, please ignore this email</p>");
            emailMessage.AppendLine("<br>");
            emailMessage.AppendLine("<p>Best regards,</p>");
            emailMessage.AppendLine("<p>Your Company</p>");
            emailMessage.AppendLine("</body>");
            emailMessage.AppendLine("</html>");

            string message = emailMessage.ToString();
            var emailConfig = new MimeMessage();
            emailConfig.From.Add(MailboxAddress.Parse(_config.GetValue<string>("Email:UserName")));
            emailConfig.To.Add(MailboxAddress.Parse(_config.GetValue<string>("Email:UserName")));
            emailConfig.Subject = "Email Confirmation";
            emailConfig.Body = new TextPart(TextFormat.Html)
            {
                Text = message
            };

            using var smtp = new SmtpClient();
            smtp.Connect("smtp.ethereal.email", int.Parse(_config.GetValue<string>("Email:Port")), MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate(_config.GetValue<string>("Email:UserName"), _config.GetValue<string>("Email:Password"));
            smtp.Send(emailConfig);
            smtp.Disconnect(true);
            return "Thank you for registering. Please check your email for the confirmation code.";
        }
    }
}
