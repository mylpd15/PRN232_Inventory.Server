using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WareSync.Api.DTOs;
using WareSync.Business;
using WareSync.Domain;
using WareSync.Services;

namespace WareSync.Api;
[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserBusiness _userBusiness;
    private readonly IMapper _mapper;
    private readonly IEmailService _emailService;
    private readonly IAuthBusiness _authBusiness;
    private readonly IConfigService _configService;

    public UsersController(IUserBusiness userBusiness, IMapper mapper, IEmailService emailService, IAuthBusiness authBusiness, IConfigService configService)
    {
        _userBusiness = userBusiness;
        _mapper = mapper;
        _emailService = emailService;
        _authBusiness = authBusiness;
        _configService = configService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var users = await _userBusiness.GetAllUsersAsync();
        var dtos = _mapper.Map<IEnumerable<UserDto>>(users);
        return Ok(dtos);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] UserDto dto)
    {
        // Check duplicate username
        if (!string.IsNullOrWhiteSpace(dto.Username))
        {
            var userByUsername = await _userBusiness.GetUserByUsernameOrEmailAsync(dto.Username);
            if (userByUsername != null)
                return BadRequest(new { message = "Username đã tồn tại." });
        }
        // Check duplicate email
        if (!string.IsNullOrWhiteSpace(dto.Email))
        {
            var userByEmail = await _userBusiness.GetUserByUsernameOrEmailAsync(dto.Email);
            if (userByEmail != null)
                return BadRequest(new { message = "Email đã tồn tại." });
        }
        var entity = _mapper.Map<AppUser>(dto);
        var created = await _userBusiness.CreateUserAsync(entity);
        // Generate One-Time Login Token
        var token = _authBusiness.GenerateOneTimeLoginToken(created);
        var loginUrl = $"{_configService.GetString("App:BaseUrl")}/auth/one-time-login?token={token}";

        // Send email
        await _emailService.SendEmailAsync(
      created.Email,
      "You're invited to join WareSync",
      $@"
    <div style='font-family: Arial, sans-serif; line-height: 1.6; color: #333;'>
        <h2 style='color: #2c3e50;'>🎉 Welcome to <strong>WareSync</strong>!</h2>
        <p>You have been invited to join the <strong>WareSync</strong> inventory management system.</p>
        <p>To get started, please log in using the one-time link below. After logging in, you will be prompted to change your password for security purposes.</p>
        <p>
            <a href='{loginUrl}' style='
                display: inline-block;
                padding: 12px 20px;
                background-color: #4CAF50;
                color: white;
                text-decoration: none;
                border-radius: 5px;
                font-weight: bold;
            '>
                👉 Log in now
            </a>
        </p>
        <p style='font-size: 0.9em; color: #888;'>Note: This link is valid for <strong>15 minutes</strong> only.</p>
        <hr style='margin-top: 30px;' />
        <p style='font-size: 0.8em; color: #aaa;'>If you were not expecting this email, please ignore it or contact the system administrator.</p>
    </div>",
      true
  );


        var result = _mapper.Map<UserDto>(created);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(Guid id, [FromBody] UserDto dto)
    {
        var entity = _mapper.Map<AppUser>(dto);
        entity.Id = id;
        var updated = await _userBusiness.UpdateUserAsync(entity);
        var result = _mapper.Map<UserDto>(updated);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _userBusiness.DisableUserAsync(id);
        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var user = await _userBusiness.GetUserByIdAsync(id);
        if (user == null) return NotFound();
        var dto = _mapper.Map<UserDto>(user);
        return Ok(dto);
    }
}