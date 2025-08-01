using WareSync.Domain;

namespace WareSync.Repositories;
public interface IInventoryRepository : IGenericRepository<Inventory>
{
    // Thêm các method đặc thù nếu cần
    Task<Inventory?> GetByProductAndWarehouseAsync(int productId, int warehouseId);
} 