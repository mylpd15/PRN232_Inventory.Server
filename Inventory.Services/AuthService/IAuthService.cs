using WareSync.Domain;

namespace WareSync.Services;
public interface IAuthService
{
<<<<<<< HEAD
    Task<AppUser> GetMe();
    Task<LoginPayloadDto> Login(UserCredentialDto dto);
    Task<LoginPayloadDto> SignInWithGoogle(GoogleSignInVM model);
    Task<LoginPayloadDto> Signup(CreateUserDto dto);
}
=======
}
>>>>>>> develop
