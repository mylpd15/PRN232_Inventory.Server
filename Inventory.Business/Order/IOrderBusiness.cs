using WareSync.Domain;
using WareSync.Domain.Enums;

namespace WareSync.Business;
public interface IOrderBusiness
{
    Task<Domain.Order> CreateOrderAsync(Domain.Order order);
    Task<Domain.Order> CreateOrderAsync(CreateOrderDto dto);
    Task<Domain.Order> UpdateOrderAsync(Domain.Order order);
    Task<Domain.Order> UpdateOrderAsync(int orderId, CreateOrderDto dto);
    Task DeleteOrderAsync(int orderId);
    Task<Domain.Order?> GetOrderByIdAsync(int orderId);
    Task<IEnumerable<Domain.Order>> GetAllOrdersAsync();
    Task<Domain.Order> UpdateOrderStatusAsync(int orderId, OrderStatus newStatus, string? rejectReason = null);
} 