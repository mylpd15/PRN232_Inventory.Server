using WareSync.Domain;

namespace WareSync.Business;
public interface IInventoryBusiness
{
    Task<Domain.Inventory> CreateInventoryAsync(Domain.Inventory inventory);
    Task<Domain.Inventory> UpdateInventoryAsync(Domain.Inventory inventory);
    Task DeleteInventoryAsync(int inventoryId);
    Task<Domain.Inventory?> GetInventoryByIdAsync(int inventoryId);
    Task<IEnumerable<Domain.Inventory>> GetAllInventoriesAsync();
    Task<Domain.Inventory?> GetByProductAndWarehouseAsync(int productId, int warehouseId);
} 