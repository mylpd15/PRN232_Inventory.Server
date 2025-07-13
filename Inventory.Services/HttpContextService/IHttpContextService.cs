using Inventory.Domain;

namespace Inventory.Services;
public interface IHttpContextService
{
    Task<AppUser?> GetAppUser();
    Task<AppUser> GetAppUserAndThrow();
}