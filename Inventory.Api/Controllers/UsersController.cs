using Microsoft.AspNetCore.Mvc;
using WareSync.Business;
using WareSync.Api.DTOs;
using AutoMapper;
using WareSync.Domain;

namespace WareSync.Api;
[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserBusiness _userBusiness;
    private readonly IMapper _mapper;
    public UsersController(IUserBusiness userBusiness, IMapper mapper)
    {
        _userBusiness = userBusiness;
        _mapper = mapper;
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