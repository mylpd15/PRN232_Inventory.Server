using WareSync.Domain;

namespace WareSync.Business;
public interface IUserBusiness
{
    Task<AppUser?> GetUserByIdAsync(Guid id);
    Task<AppUser?> GetUserByUsernameOrEmailAsync(string usernameOrEmail);
    Task<AppUser> CreateUserAsync(AppUser appUser);
    Task<AppUser> UpdateUserAsync(AppUser appUser);
    Task<AppUser?> DisableUserAsync(Guid id);
    Task<IEnumerable<AppUser>> GetAllUsersAsync();
    Task AssignRoleAsync(Guid userId, string role);
    // ... các method khác nếu cần
} 