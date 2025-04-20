using Application.Abstracts.Services;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Infrastructure.Services.MailServices
{
    public class MailService : IMailService
    {
        readonly IConfiguration _configuration;

        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendMailAsync(string to, string subject, string body, bool isBodyHtml = true)
        {
            await SendMailAsync(to, subject, body, isBodyHtml);
        }

        public async Task SendMailAsync(List<string> to, string subject, string body, bool isBodyHtml = true)
        {
            MailMessage mail = new MailMessage();
            mail.IsBodyHtml = isBodyHtml;
            foreach (var item in to)
                mail.To.Add(item);
            mail.Subject = subject;
            mail.Body = body;

            mail.From = new(_configuration["Mail:Username"], "Api", Encoding.UTF8);

            SmtpClient smtp = new SmtpClient();
            smtp.Credentials = new NetworkCredential(_configuration["Mail:Username"], _configuration["Mail:Password"]);
            smtp.Port = Convert.ToInt32(_configuration["Mail:Port"]);
            smtp.EnableSsl = true;
            smtp.Host = _configuration["Mail:Host"];
            await smtp.SendMailAsync(mail);
        }

        public async Task SendPasswordResetMailAsync(string to, string userId, string resetToken)
        {
            string resetLink = $"Hi! <br> If you want reset password, yo have a link for reset password <br> <strong> <a target='blank' href='....../{userId}/{resetToken}'></a> Touch for new password </strong> <br><br><br> Montela";

            await SendMailAsync(to, "Reset Password", resetLink);
        }
    }
}
