using Microsoft.EntityFrameworkCore;
using WareSync.Domain;

namespace WareSync.Repositories;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Product: ProductCode unique, required
        modelBuilder.Entity<Product>()
            .HasIndex(p => p.ProductCode)
            .IsUnique();
        modelBuilder.Entity<Product>()
            .Property(p => p.ProductCode)
            .IsRequired()
            .HasMaxLength(100);
        // Audit fields default value
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(AuditableEntity).IsAssignableFrom(entity.ClrType))
            {
                modelBuilder.Entity(entity.ClrType)
                    .Property(nameof(AuditableEntity.CreatedDate))
                    .HasDefaultValueSql("GETDATE()");
                modelBuilder.Entity(entity.ClrType)
                    .Property(nameof(AuditableEntity.UpdatedDate))
                    .HasDefaultValueSql("GETDATE()");
            }
        }
        // Order - OrderDetail: 1-nhiều, cascade delete
        modelBuilder.Entity<Order>()
            .HasMany(o => o.OrderDetails)
            .WithOne(od => od.Order)
            .HasForeignKey(od => od.OrderID)
            .OnDelete(DeleteBehavior.Cascade);
        // Provider - Order: 1-nhiều
        modelBuilder.Entity<Provider>()
            .HasMany(p => p.Orders)
            .WithOne(o => o.Provider)
            .HasForeignKey(o => o.ProviderID)
            .OnDelete(DeleteBehavior.Restrict);
        // Product - OrderDetail: 1-nhiều
        modelBuilder.Entity<Product>()
            .HasMany(p => p.OrderDetails)
            .WithOne(od => od.Product)
            .HasForeignKey(od => od.ProductID)
            .OnDelete(DeleteBehavior.Restrict);
        // Product - Inventory: 1-nhiều
        modelBuilder.Entity<Product>()
            .HasMany(p => p.Inventories)
            .WithOne(i => i.Product)
            .HasForeignKey(i => i.ProductID)
            .OnDelete(DeleteBehavior.Cascade);
        // Warehouse - Inventory: 1-nhiều
        modelBuilder.Entity<Warehouse>()
            .HasMany(w => w.Inventories)
            .WithOne(i => i.Warehouse)
            .HasForeignKey(i => i.WarehouseID)
            .OnDelete(DeleteBehavior.Cascade);
        // Warehouse - Location: nhiều-1
        modelBuilder.Entity<Location>()
            .HasMany(l => l.Warehouses)
            .WithOne(w => w.Location)
            .HasForeignKey(w => w.LocationID)
            .OnDelete(DeleteBehavior.Restrict);
        // Customer - Delivery: 1-nhiều
        modelBuilder.Entity<Customer>()
            .HasMany(c => c.Deliveries)
            .WithOne(d => d.Customer)
            .HasForeignKey(d => d.CustomerID)
            .OnDelete(DeleteBehavior.Restrict);
        // Delivery - DeliveryDetail: 1-nhiều, cascade delete
        modelBuilder.Entity<Delivery>()
            .HasMany(d => d.DeliveryDetails)
            .WithOne(dd => dd.Delivery)
            .HasForeignKey(dd => dd.DeliveryID)
            .OnDelete(DeleteBehavior.Cascade);
        // Product - DeliveryDetail: 1-nhiều
        modelBuilder.Entity<Product>()
            .HasMany(p => p.DeliveryDetails)
            .WithOne(dd => dd.Product)
            .HasForeignKey(dd => dd.ProductID)
            .OnDelete(DeleteBehavior.Restrict);
        // OrderDetail - Transfer: 1-nhiều
        modelBuilder.Entity<OrderDetail>()
            .HasMany(od => od.Transfers)
            .WithOne(t => t.OrderDetail)
            .HasForeignKey(t => t.OrderDetailID)
            .OnDelete(DeleteBehavior.Cascade);
        // Warehouse - Transfer: 1-nhiều
        modelBuilder.Entity<Warehouse>()
            .HasMany(w => w.Transfers)
            .WithOne(t => t.Warehouse)
            .HasForeignKey(t => t.WarehouseID)
            .OnDelete(DeleteBehavior.Restrict);
        // Inventory - InventoryLog: 1 - nhiều
        modelBuilder.Entity<Inventory>()
            .HasMany(i => i.InventoryLogs)
            .WithOne(l => l.Inventory)
            .HasForeignKey(l => l.InventoryID)
            .OnDelete(DeleteBehavior.Cascade);
        // Order - Warehouse: many-to-one, restrict delete
        modelBuilder.Entity<Order>()
            .HasOne(o => o.Warehouse)
            .WithMany()
            .HasForeignKey(o => o.WarehouseID)
            .OnDelete(DeleteBehavior.Restrict);

    }
    public DbSet<AppUser> AppUsers { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Inventory> Inventories { get; set; }
    public DbSet<InventoryLog> InventoryLogs { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<Transfer> Transfers { get; set; }
    public DbSet<Provider> Providers { get; set; }
    public DbSet<Warehouse> Warehouses { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Delivery> Deliveries { get; set; }
    public DbSet<DeliveryDetail> DeliveryDetails { get; set; }

}
