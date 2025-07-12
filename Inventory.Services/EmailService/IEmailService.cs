using TeachMate.Domain;

namespace TeachMate.Services;
public interface IEmailService
{
    void SendTestEmail(AppUser user);
}