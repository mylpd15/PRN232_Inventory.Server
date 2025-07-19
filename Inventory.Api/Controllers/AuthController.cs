using Microsoft.AspNetCore.Mvc;
using WareSync.Domain;
using WareSync.Business;

namespace WareSync.Api;
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthBusiness _authBusiness;

    public AuthController(IAuthBusiness authBusiness)
    {
        _authBusiness = authBusiness;
    }

    /// <summary>
    /// Login with Username and Password
    /// </summary>
    [HttpPost("Login")]
    public async Task<ActionResult<LoginPayloadDto>> Login(UserCredentialDto dto)
    {
        return Ok(await _authBusiness.Login(dto));
    }

    /// <summary>
    /// Signin with google
    /// </summary>
    [HttpPost("GoogleSignIn")]
    public async Task<ActionResult<LoginPayloadDto>> GoogleSignIn(GoogleSignInVM dto)
    {
        return Ok(await _authBusiness.SignInWithGoogle(dto));
    }

    /// <summary>
    /// Signup with Username and Password
    /// </summary>
    [HttpPost("SignUp")]
    public async Task<ActionResult<LoginPayloadDto>> Signup(CreateUserDto dto)
    {
        return Ok(await _authBusiness.Signup(dto));
    }

    /// <summary>
    /// Get Current User
    /// </summary>
    [HttpGet("Me")]
    public async Task<ActionResult<AppUser>> GetMe()
    {
        return Ok(await _authBusiness.GetMe());
    }
}
