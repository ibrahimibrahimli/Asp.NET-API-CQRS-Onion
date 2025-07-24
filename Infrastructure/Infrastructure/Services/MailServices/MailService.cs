using Application.Common.Interfaces;
using Domain.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
namespace Infrustructure.Services
{
    public class MailService : IMailService
    {
        readonly ILogger<MailService> _logger;
        readonly SmtpSettings _settings;
        public MailService(ILogger<MailService> logger, IOptions<SmtpSettings> options)
        {
            _logger = logger;
            _settings = options.Value;
        }

        public async Task SendAsync(string to, string subject, string htmlBody)
        {
            await SendAsync(to, subject, htmlBody, _settings.FromEmail, _settings.FromName);
        }

        public async Task SendAsync(string to, string subject, string htmlBody, string from, string displayName)
        {
            var message = new MailMessage
            {
                From = new MailAddress(from ?? _settings.FromEmail, displayName ?? _settings.FromName),
                Subject = subject,
                Body = htmlBody,
                IsBodyHtml = true
            };

            message.To.Add(to);

            using var smtp = new SmtpClient(_settings.Host, _settings.Port)
            {
                Credentials = new NetworkCredential(_settings.Username, _settings.Password),
                EnableSsl = _settings.EnableSsl,
            };

            try
            {
                await smtp.SendMailAsync(message);
                _logger.LogInformation($"Email sent to {to}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Email could not be sent to {to}");
                throw;
            }
        }

        public async Task SendWithAttachmentAsync(string to, string subject, string htmlBody, List<(byte[] Content, string FileName, string MimeType)> attachments)
        {
            using var message = new MailMessage
            {
                From = new MailAddress(_settings.FromEmail, _settings.FromName),
                Subject = subject,
                Body = htmlBody,
                IsBodyHtml = true
            };

            message.To.Add(to);

            foreach (var attachment in attachments)
            {
                var stream = new MemoryStream(attachment.Content);
                message.Attachments.Add(new Attachment(stream, attachment.FileName, attachment.MimeType));
            }

            using var smtp = new SmtpClient(_settings.Host, _settings.Port)
            {
                Credentials = new NetworkCredential(_settings.Username, _settings.Password),
                EnableSsl = _settings.EnableSsl
            };

            try
            {
                await smtp.SendMailAsync(message);
                _logger.LogInformation($"Email with attachments sent to {to}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to send email with attachments to {to}");
                throw;
            }
        }
    }


}

//to do - Mail servisləri ayır və MailService servisini hər birini ayrıca istifadə edə biləcək səviyyəyə gətir!