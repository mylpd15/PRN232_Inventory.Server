using Microsoft.EntityFrameworkCore;
using WareSync.Domain;

namespace WareSync.Repositories;
public class InventoryRepository : GenericRepository<Inventory>, IInventoryRepository
{
   
    public InventoryRepository(DataContext context) : base(context) { }

    public async Task<Inventory?> GetByProductAndWarehouseAsync(int productId, int warehouseId)
    {
        return await _context.Inventories.FirstOrDefaultAsync(i => i.ProductID == productId && i.WarehouseID == warehouseId);
    }
} 