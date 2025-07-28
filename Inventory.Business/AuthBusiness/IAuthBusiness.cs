using WareSync.Domain;

namespace WareSync.Business;
public interface IAuthBusiness
{
    Task<AppUser> GetMe();
    Task<LoginPayloadDto> Login(UserCredentialDto dto);
    Task<LoginPayloadDto> SignInWithGoogle(GoogleSignInVM model);
    Task<LoginPayloadDto> Signup(CreateUserDto dto);
    string GenerateOneTimeLoginToken(AppUser appUser);
    TokenPayloadDto GenerateTokenPayload(AppUser appUser);
} 