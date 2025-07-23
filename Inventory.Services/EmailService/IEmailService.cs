using WareSync.Domain;

namespace WareSync.Services;
public interface IEmailService
{
    void SendTestEmail(AppUser user);
}
