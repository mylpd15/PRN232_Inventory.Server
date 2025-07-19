using WareSync.Domain;
using Microsoft.EntityFrameworkCore;

namespace WareSync.Repositories;
public class UserRepository : GenericRepository<AppUser>, IUserRepository
{
    private readonly DataContext _context;
    public UserRepository(DataContext context) : base(context)
    {
        _context = context;
    }
    public async Task<AppUser?> GetUserByUsernameOrEmailAsync(string usernameOrEmail)
    {
        return await _context.AppUsers.FirstOrDefaultAsync(u => u.Username == usernameOrEmail || u.Email == usernameOrEmail);
    }
    // ... các method khác nếu cần
}