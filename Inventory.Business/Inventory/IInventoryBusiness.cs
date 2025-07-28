using WareSync.Domain;

namespace WareSync.Business;
public interface IInventoryBusiness
{
    Task<Inventory> CreateInventoryAsync(Inventory inventory);
    Task<Inventory> UpdateInventoryAsync(Inventory inventory);
    Task DeleteInventoryAsync(int inventoryId);
    Task<Inventory?> GetInventoryByIdAsync(int inventoryId);
    Task<IEnumerable<Inventory>> GetAllInventoriesAsync();
    Task<Inventory?> GetByProductAndWarehouseAsync(int productId, int warehouseId);
} 