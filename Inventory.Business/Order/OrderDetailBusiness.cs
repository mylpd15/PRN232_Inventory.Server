using WareSync.Domain;
using WareSync.Repositories;

namespace WareSync.Business;
public class OrderDetailBusiness : IOrderDetailBusiness
{
    private readonly IOrderDetailRepository _orderDetailRepository;
    public OrderDetailBusiness(IOrderDetailRepository orderDetailRepository)
    {
        _orderDetailRepository = orderDetailRepository;
    }
    public async Task<OrderDetail> CreateOrderDetailAsync(OrderDetail detail)
    {
        await _orderDetailRepository.AddAsync(detail);
        return detail;
    }
    public async Task<OrderDetail> UpdateOrderDetailAsync(OrderDetail detail)
    {
        await _orderDetailRepository.UpdateAsync(detail);
        return detail;
    }
    public async Task DeleteOrderDetailAsync(int orderDetailId)
    {
        var detail = await _orderDetailRepository.GetByIdAsync(orderDetailId);
        if (detail != null)
            _orderDetailRepository.Remove(detail);
    }
    public async Task<OrderDetail?> GetOrderDetailByIdAsync(int orderDetailId)
    {
        return await _orderDetailRepository.GetByIdAsync(orderDetailId);
    }
    public async Task<IEnumerable<OrderDetail>> GetAllOrderDetailsAsync()
    {
        return await _orderDetailRepository.GetAllAsync();
    }
} 