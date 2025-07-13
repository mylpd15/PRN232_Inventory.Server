using Inventory.Domain;

namespace Inventory.Services;
public interface IEmailService
{
    void SendTestEmail(AppUser user);
}