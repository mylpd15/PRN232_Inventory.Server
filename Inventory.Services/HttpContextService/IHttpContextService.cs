using WareSync.Domain;

namespace WareSync.Services;
public interface IHttpContextService
{
<<<<<<< HEAD
    Task<AppUser?> GetAppUser();
    Task<AppUser> GetAppUserAndThrow();
}
=======
    Guid? GetUserId();
}
>>>>>>> develop
