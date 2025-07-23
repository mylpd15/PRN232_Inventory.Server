using WareSync.Domain;
using WareSync.Repositories;
using WareSync.Services;
using BC = BCrypt.Net.BCrypt;

namespace WareSync.Business;
public class AuthBusiness : IAuthBusiness
{
    private readonly IUserBusiness _userBusiness;
    private readonly IHttpContextService _httpContextService;
    private readonly IGoogleAuthService _googleAuthService;
    private readonly IConfigService _configService;

    public AuthBusiness(IUserBusiness userBusiness, IConfigService configService, IHttpContextService httpContextService, IGoogleAuthService googleAuthService)
    {
        _userBusiness = userBusiness;
        _configService = configService;
        _httpContextService = httpContextService;
        _googleAuthService = googleAuthService;
    }
    public async Task<AppUser> GetMe()
    {
        var userId = _httpContextService.GetUserId();
        if (userId == null)
            throw new UnauthorizedException();
        var user = await _userBusiness.GetUserByIdAsync(userId.Value);
        return user ?? throw new UnauthorizedException();
    }
    public async Task<LoginPayloadDto> Login(UserCredentialDto dto)
    {
        var user = await _userBusiness.GetUserByUsernameOrEmailAsync(dto.Username);
        if (user is null || !BC.Verify(dto.Password, user.Password))
        {
            throw new UnauthorizedException("Username or Password is not correct.");
        }
        return new LoginPayloadDto
        {
            AccessToken = GenerateTokenPayload(user),
            AppUser = user
        };
    }
    public async Task<LoginPayloadDto> Signup(CreateUserDto dto)
    {
        var user = await _userBusiness.GetUserByUsernameOrEmailAsync(dto.Username);
        if (user != null)
        {
            throw new ConflictException("Username or Email already exists.");
        }
        var appUser = new AppUser
        {
            Username = dto.Username,
            DisplayName = dto.Username,
            Password = dto.Password,
            UserRole = dto.UserRole,
        };
        appUser = await _userBusiness.CreateUserAsync(appUser);
        return new LoginPayloadDto
        {
            AccessToken = GenerateTokenPayload(appUser),
            AppUser = appUser
        };
    }
    public async Task<LoginPayloadDto> SignInWithGoogle(GoogleSignInVM model)
    {
        var userInfo = await _googleAuthService.GoogleSignIn(model);
        var appUser = await _userBusiness.GetUserByUsernameOrEmailAsync(userInfo.Email);
        if (appUser == null)
        {
            appUser = new AppUser
            {
                Email = userInfo.Email,
                DisplayName = userInfo.FirstName + " " + userInfo.LastName,
                UserRole = model.UserRole
            };
            appUser = await _userBusiness.CreateUserAsync(appUser);
        }
        return new LoginPayloadDto
        {
            AccessToken = GenerateTokenPayload(appUser),
            AppUser = appUser
        };
    }
    private TokenPayloadDto GenerateTokenPayload(AppUser appUser)
    {
        return new TokenPayloadDto
        {
            ExpiresIn = _configService.GetInt("Jwt:Lifetime:AccessToken"),
            AccessToken = GenerateToken(appUser, AuthToken.AccessToken),
            RefreshToken = GenerateToken(appUser, AuthToken.RefreshToken)
        };
    }
    private string GenerateToken(AppUser appUser, AuthToken tokenType)
    {
        var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
        var secretKey = System.Text.Encoding.UTF8.GetBytes(_configService.GetString("Jwt:SecretKey"));
        var claims = new List<System.Security.Claims.Claim>
        {
            new System.Security.Claims.Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new System.Security.Claims.Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub, _configService.GetString("Jwt:Subject")),
            new System.Security.Claims.Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
            new System.Security.Claims.Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Typ, tokenType.ToString()),
            new System.Security.Claims.Claim("UserId", appUser.Id.ToString()),
            new System.Security.Claims.Claim("Username", appUser.Username ?? ""),
            new System.Security.Claims.Claim("Email", appUser.Email ?? ""),
            new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Role, appUser.ToCustomRole())
        };
        var lifetime = tokenType == AuthToken.AccessToken
            ? _configService.GetInt("Jwt:Lifetime:AccessToken")
            : _configService.GetInt("Jwt:Lifetime:RefreshToken");
        var tokenDescriptor = new Microsoft.IdentityModel.Tokens.SecurityTokenDescriptor
        {
            Subject = new System.Security.Claims.ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddSeconds(lifetime),
            Issuer = _configService.GetString("Jwt:Issuer"),
            Audience = _configService.GetString("Jwt:Audience"),
            SigningCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(
                new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(secretKey),
                Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256)
        };
        var jwt = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(jwt);
    }
} 