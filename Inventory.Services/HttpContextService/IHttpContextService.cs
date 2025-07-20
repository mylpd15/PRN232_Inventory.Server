using WareSync.Domain;

namespace WareSync.Services;
public interface IHttpContextService
{
    Guid? GetUserId();
}