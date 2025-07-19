using WareSync.Domain;

namespace WareSync.Repositories;
public interface IUserRepository : IGenericRepository<AppUser>
{
    Task<AppUser?> GetUserByUsernameOrEmailAsync(string usernameOrEmail);
    // ... các method khác nếu cần
}