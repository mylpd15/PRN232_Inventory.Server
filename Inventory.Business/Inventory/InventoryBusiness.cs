using WareSync.Domain;
using WareSync.Repositories.InventoryRepository;

namespace WareSync.Business;
public class InventoryBusiness : IInventoryBusiness
{
    private readonly IInventoryRepository _inventoryRepository;
    public InventoryBusiness(IInventoryRepository inventoryRepository)
    {
        _inventoryRepository = inventoryRepository;
    }
    public async Task<Inventory> CreateInventoryAsync(Inventory inventory)
    {
        await _inventoryRepository.AddAsync(inventory);
        return inventory;
    }
    public async Task<Inventory> UpdateInventoryAsync(Inventory inventory)
    {
        await _inventoryRepository.UpdateAsync(inventory);
        return inventory;
    }
    public async Task DeleteInventoryAsync(int inventoryId)
    {
        var inventory = await _inventoryRepository.GetByIdAsync(inventoryId);
        if (inventory != null)
            _inventoryRepository.Remove(inventory);
    }
    public async Task<Inventory?> GetInventoryByIdAsync(int inventoryId)
    {
        return await _inventoryRepository.GetByIdAsync(inventoryId);
    }
    public async Task<IEnumerable<Inventory>> GetAllInventoriesAsync()
    {
        return await _inventoryRepository.GetAllAsync();
    }
} 