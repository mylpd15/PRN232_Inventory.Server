using Microsoft.EntityFrameworkCore;
using Inventory.Domain;

namespace Inventory.Services;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
    public DbSet<AppUser> AppUsers { get; set; }

}
