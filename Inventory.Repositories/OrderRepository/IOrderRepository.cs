using WareSync.Domain;

namespace WareSync.Repositories;
public interface IOrderRepository : IGenericRepository<Order>
{
    Task<Order?> GetByIdWithDetailsAsync(int orderId);
    Task<IEnumerable<Order>> GetAllOrdersWithProviderAndWarehouse();
} 