using WareSync.Domain;

namespace WareSync.Business;
public interface IOrderBusiness
{
    Task<Domain.Order> CreateOrderAsync(Domain.Order order);
    Task<Domain.Order> UpdateOrderAsync(Domain.Order order);
    Task DeleteOrderAsync(int orderId);
    Task<Domain.Order?> GetOrderByIdAsync(int orderId);
    Task<IEnumerable<Domain.Order>> GetAllOrdersAsync();
} 