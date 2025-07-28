using Microsoft.AspNetCore.Mvc;
using WareSync.Domain;
using WareSync.Business;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using WareSync.Services;
using Microsoft.AspNetCore.Authorization;
using WareSync.Api.DTOs;
using AutoMapper;

namespace WareSync.Api;
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthBusiness _authBusiness;
    private readonly IConfigService _configService;
    private readonly IUserBusiness _userBusiness;
    private readonly IMapper _mapper;

    public AuthController(IAuthBusiness authBusiness, IConfigService configService, IUserBusiness userBusiness, IMapper mapper)
    {
        _authBusiness = authBusiness;
        _configService = configService;
        _userBusiness = userBusiness;
        _mapper = mapper;
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
    public async Task<ActionResult<UserDto>> GetMe()
    {
        var user = await _authBusiness.GetMe();
        var dto = _mapper.Map<UserDto>(user);
        return Ok(dto);
    }

    [HttpPost("one-time-login")]
    public async Task<IActionResult> OneTimeLogin([FromQuery] string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_configService.GetString("Jwt:SecretKey"));

        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidIssuer = _configService.GetString("Jwt:Issuer"),
                ValidAudience = _configService.GetString("Jwt:Audience"),
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(key)
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var userId = jwtToken.Claims.First(x => x.Type == "UserId").Value;

            var user = await _userBusiness.GetUserByIdAsync(Guid.Parse(userId));
            if (user == null)
                return Unauthorized();

            return Ok(new LoginPayloadDto
            {
                AccessToken = _authBusiness.GenerateTokenPayload(user),
                AppUser = user
            });
        }
        catch
        {
            return Unauthorized(new { message = "Token không hợp lệ hoặc đã hết hạn." });
        }
    }

    [HttpPut("ChangePassWord")]
    [Authorize] 
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto dto)
    {
        var userId = User.FindFirst("UserId")?.Value;
        if (userId == null)
            return Unauthorized();

        var user = await _userBusiness.GetUserByIdAsync(Guid.Parse(userId));
        if (user == null)
            return NotFound("User not found.");

        var isOldPasswordValid = BCrypt.Net.BCrypt.Verify(dto.Old_Password, user.Password);
        if (!isOldPasswordValid)
            return BadRequest(new { message = "Mật khẩu cũ không đúng." });

        if (dto.New_Password != dto.Confirm_Password)
            return BadRequest(new { message = "Mật khẩu xác nhận không khớp." });

        user.Password = BCrypt.Net.BCrypt.HashPassword(dto.New_Password);
        await _userBusiness.UpdateUserAsync(user);

        return Ok(new { message = "Đổi mật khẩu thành công." });
    }


}
