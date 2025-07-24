namespace Application.Common.Interfaces
{
    public interface IMailService
    {
        Task SendAsync(string to, string subject, string htmlBody);
        Task SendAsync(string to, string subject, string htmlBody, string? from, string? displayName);
        Task SendWithAttachmentAsync(string to, string subject, string htmlBody, List<(byte[] Content, string FileName, string MimeType)> attachments);
    }
}
