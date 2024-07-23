using System.Net;
using System.Net.Mail;

namespace TaskBack.Models.services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
    public class EmailSender(IConfiguration configuration, ILogger<EmailSender> logger) : IEmailSender
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly ILogger<EmailSender> _logger = logger;

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            try
            {
                var smtpClient = new SmtpClient(_configuration["EmailSettings:SmtpServer"])
                {
                    Port = int.Parse(_configuration["EmailSettings:Port"]!),
                    Credentials = new NetworkCredential(_configuration["EmailSettings:Username"], _configuration["EmailSettings:Password"]),
                    EnableSsl = bool.Parse(_configuration["EmailSettings:EnableSsl"]!),
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_configuration["EmailSettings:From"]!),
                    Subject = subject,
                    Body = message,
                    IsBodyHtml = true,
                };
                mailMessage.To.Add(email);

                await smtpClient.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending email.");
            }
        }
    }
}
