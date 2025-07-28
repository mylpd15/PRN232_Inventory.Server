using WareSync.Domain;

namespace WareSync.Business
{
    public interface IInventoryLogBusiness
    {
        Task<IEnumerable<InventoryLog>> GetInventoryLogsAsync();
        Task<InventoryLog?> GetInventoryLogByIdAsync(int inventoryLogId);
        Task<InventoryLog> CreateInventoryLogAsync(InventoryLog inventoryLog);
    }
}
