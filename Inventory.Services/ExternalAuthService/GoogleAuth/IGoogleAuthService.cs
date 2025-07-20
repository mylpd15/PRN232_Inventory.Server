using WareSync.Domain;

namespace WareSync.Services;
public interface IGoogleAuthService
{
    Task<ExternalLoginUserInfo> GoogleSignIn(GoogleSignInVM model);
}