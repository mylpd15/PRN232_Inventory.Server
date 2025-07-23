using WareSync.Domain;
using WareSync.Repositories;
using BC = BCrypt.Net.BCrypt;

namespace WareSync.Business;
public class UserBusiness : IUserBusiness
{
    private readonly IUserRepository _userRepository;
    public UserBusiness(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<AppUser?> GetUserByIdAsync(Guid id)
    {
        return await _userRepository.GetByIdAsync(id);
    }
    public async Task<AppUser?> GetUserByUsernameOrEmailAsync(string usernameOrEmail)
    {
        return await _userRepository.GetUserByUsernameOrEmailAsync(usernameOrEmail);
    }
    public async Task<AppUser> CreateUserAsync(AppUser appUser)
    {
        if (string.IsNullOrEmpty(appUser.Password))
        {
            appUser.Password = "abc@123";
        }
        if (appUser.Password != null)
        {
            appUser.Password = BC.HashPassword(appUser.Password);
        }
        return await _userRepository.AddAsync(appUser);
    }
    public async Task<AppUser> UpdateUserAsync(AppUser appUser)
    {
        return await _userRepository.UpdateAsync(appUser);
    }
    public async Task DeleteUserAsync(Guid userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user != null)
            _userRepository.Remove(user);
    }
    public async Task<AppUser?> DisableUserAsync(Guid id)
    {
        var appUser = await _userRepository.GetByIdAsync(id);
        if (appUser != null)
        {
            appUser.IsDisabled = true;
            await _userRepository.UpdateAsync(appUser);
        }
        return appUser;
    }
    public async Task<IEnumerable<AppUser>> GetAllUsersAsync()
    {
        return await _userRepository.GetAllAsync();
    }
    public async Task AssignRoleAsync(Guid userId, string role)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user != null)
        {
            if (Enum.TryParse<UserRole>(role, out var userRole))
            {
                user.UserRole = userRole;
                await _userRepository.UpdateAsync(user);
            }
        }
    }
} 