using WareSync.Domain;
using WareSync.Repositories;

namespace WareSync.Business;
public class WarehouseBusiness : IWarehouseBusiness
{
    private readonly IWarehouseRepository _warehouseRepository;
    public WarehouseBusiness(IWarehouseRepository warehouseRepository)
    {
        _warehouseRepository = warehouseRepository;
    }
    public async Task<Warehouse> CreateWarehouseAsync(Warehouse warehouse)
    {
        await _warehouseRepository.AddAsync(warehouse);
        return warehouse;
    }
    public async Task<Warehouse> UpdateWarehouseAsync(Warehouse warehouse)
    {
        await _warehouseRepository.UpdateAsync(warehouse);
        return warehouse;
    }
    public async Task DeleteWarehouseAsync(int warehouseId)
    {
        var warehouse = await _warehouseRepository.GetByIdAsync(warehouseId);
        if (warehouse != null)
            _warehouseRepository.Remove(warehouse);
    }
    public async Task<Warehouse?> GetWarehouseByIdAsync(int warehouseId)
    {
        return await _warehouseRepository.GetByIdAsync(warehouseId);
    }
    public async Task<IEnumerable<Warehouse>> GetAllWarehousesAsync()
    {
        return await _warehouseRepository.GetAllAsync();
    }
} 