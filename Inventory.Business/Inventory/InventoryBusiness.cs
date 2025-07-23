using WareSync.Domain;
using WareSync.Repositories;

namespace WareSync.Business;
public class InventoryBusiness : IInventoryBusiness
{
    private readonly IInventoryRepository _inventoryRepository;
    private readonly IInventoryLogBusiness _inventoryLogBusiness;
    public InventoryBusiness(IInventoryRepository inventoryRepository, IInventoryLogBusiness inventoryLogBusiness)
    {
        _inventoryRepository = inventoryRepository;
        _inventoryLogBusiness = inventoryLogBusiness;
    }
    public async Task<Inventory> CreateInventoryAsync(Inventory inventory)
    {
        await _inventoryRepository.AddAsync(inventory);

        await _inventoryLogBusiness.CreateInventoryLogAsync(new InventoryLog
        {
            InventoryID = inventory.InventoryID,
            ActionType = "Create",
            ChangedQuantity = inventory.QuantityAvailable,
            Description = "Create New Inventory"
        });

        return inventory;
    }
    public async Task<Inventory> UpdateInventoryAsync(Inventory inventory)
    {
        var old = await _inventoryRepository.GetByIdAsync(inventory.InventoryID);
        var diff = inventory.QuantityAvailable - (old?.QuantityAvailable ?? 0);

        await _inventoryRepository.UpdateAsync(inventory);

        await _inventoryLogBusiness.CreateInventoryLogAsync(new InventoryLog
        {
            InventoryID = inventory.InventoryID,
            ActionType = "Update",
            ChangedQuantity = diff,
            Description = $"Updated Inventory from {old?.QuantityAvailable} → {inventory.QuantityAvailable}"
        });

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