using Microsoft.EntityFrameworkCore;
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
        var old = await _inventoryRepository.GetByIdAsync(inventory.InventoryID) ?? throw new KeyNotFoundException($"Inventory {inventory.InventoryID} not existed");

        _inventoryRepository.Detach(old);

        var diff = inventory.QuantityAvailable - (old?.QuantityAvailable ?? 0);

        //old.QuantityAvailable = inventory.QuantityAvailable;
        //old.MinimumStockLevel = inventory.MinimumStockLevel;
        //old.MaximumStockLevel = inventory.MaximumStockLevel;
        //old.ReorderPoint = inventory.ReorderPoint;
        //old.ProductID = inventory.ProductID;
        //old.WarehouseID = inventory.WarehouseID;

        //await _inventoryRepository.SaveChangesAsync();

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
        return await _inventoryRepository
         .Query()
         .Include(i => i.Product)
         .Include(i => i.Warehouse)
         .Include(i => i.InventoryLogs)
         .FirstOrDefaultAsync(i => i.InventoryID == inventoryId);
    }

    public async Task<IEnumerable<Inventory>> GetAllInventoriesAsync()
    {
        return await _inventoryRepository
         .Query()
         .Include(i => i.Product)
         .Include(i => i.Warehouse)
         .Include(i => i.InventoryLogs)
         .ToListAsync();
    }

    public async Task<Inventory?> GetByProductAndWarehouseAsync(int productId, int warehouseId)
    {
        return await _inventoryRepository.GetByProductAndWarehouseAsync(productId, warehouseId);
    }
} 