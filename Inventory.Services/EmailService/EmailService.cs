using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using WareSync.Domain;

namespace WareSync.Services;
public class EmailService : IEmailService
{
    private readonly EmailConfig _emailConfig;
    public EmailService(IOptions<EmailConfig> emailConfig)
    {
        _emailConfig = emailConfig.Value;
    }
    public void SendTestEmail(AppUser user)
    {
        if (user.Email == null)
        {
            return;
        }

        MailMessage message = new MailMessage();
        message.From = new MailAddress(_emailConfig.AppEmail);
        message.Subject = "Test Email";
        message.To.Add(new MailAddress(user.Email));
        message.IsBodyHtml = true;
        message.Body = "<html><body>Test Content</body></html>";

        var smtpClient = new SmtpClient("smtp.gmail.com")
        {
            Port = 587,
            Credentials = new NetworkCredential(_emailConfig.AppEmail, _emailConfig.AppPassword),
            EnableSsl = true
        };

        smtpClient.Send(message);
    }

}
