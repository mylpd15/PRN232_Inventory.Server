using Inventory.Domain;

namespace Inventory.Services;
public interface IAuthService
{
    Task<AppUser> GetMe();
    Task<LoginPayloadDto> Login(UserCredentialDto dto);
    Task<LoginPayloadDto> SignInWithGoogle(GoogleSignInVM model);
    Task<LoginPayloadDto> Signup(CreateUserDto dto);
}
