using WareSync.Domain;

namespace WareSync.Business;
public interface IWarehouseBusiness
{
    Task<Domain.Warehouse> CreateWarehouseAsync(Domain.Warehouse warehouse);
    Task<Domain.Warehouse> UpdateWarehouseAsync(Domain.Warehouse warehouse);
    Task DeleteWarehouseAsync(int warehouseId);
    Task<Domain.Warehouse?> GetWarehouseByIdAsync(int warehouseId);
    Task<IEnumerable<Domain.Warehouse>> GetAllWarehousesAsync();
} 