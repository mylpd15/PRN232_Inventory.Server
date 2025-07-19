using WareSync.Domain;

namespace WareSync.Services;
public interface IEmailService
{
    void SendTestEmail(AppUser user);
    Task SendEmailAsync(string toEmail, string subject, string body, bool isHtml = true);
}