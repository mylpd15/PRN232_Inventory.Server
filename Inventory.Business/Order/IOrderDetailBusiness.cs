using WareSync.Domain;

namespace WareSync.Business;
public interface IOrderDetailBusiness
{
    Task<OrderDetail> CreateOrderDetailAsync(OrderDetail detail);
    Task<OrderDetail> UpdateOrderDetailAsync(OrderDetail detail);
    Task DeleteOrderDetailAsync(int orderDetailId);
    Task<OrderDetail?> GetOrderDetailByIdAsync(int orderDetailId);
    Task<IEnumerable<OrderDetail>> GetAllOrderDetailsAsync();
} 