using Inventory.Domain;

namespace Inventory.Services;
public interface IGoogleAuthService
{
    Task<ExternalLoginUserInfo> GoogleSignIn(GoogleSignInVM model);
}
