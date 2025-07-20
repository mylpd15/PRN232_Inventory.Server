using WareSync.Domain;
using WareSync.Repositories;
using WareSync.Business;

namespace WareSync.Business;
public class OrderBusiness : IOrderBusiness
{
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderDetailRepository _orderDetailRepository;
    public OrderBusiness(IOrderRepository orderRepository, IOrderDetailRepository orderDetailRepository)
    {
        _orderRepository = orderRepository;
        _orderDetailRepository = orderDetailRepository;
    }
    public async Task<Order> CreateOrderAsync(CreateOrderDto dto)
    {
        var order = new Order
        {
            OrderDate = dto.OrderDate,
            ProviderID = dto.ProviderID,
            OrderDetails = new List<OrderDetail>()
        };
        await _orderRepository.AddAsync(order);
        foreach (var detailDto in dto.OrderDetails)
        {
            var detail = new OrderDetail
            {
                ProductID = detailDto.ProductID,
                OrderQuantity = detailDto.OrderQuantity,
                ExpectedDate = detailDto.ExpectedDate,
                OrderID = order.OrderID
            };
            await _orderDetailRepository.AddAsync(detail);
            order.OrderDetails.Add(detail);
        }
        return order;
    }
    public async Task<Order> UpdateOrderAsync(int orderId, CreateOrderDto dto)
    {
        var order = await _orderRepository.GetByIdAsync(orderId);
        if (order == null) throw new Exception("Order not found");
        order.OrderDate = dto.OrderDate;
        order.ProviderID = dto.ProviderID;
        var oldDetails = await _orderDetailRepository.FindAsync(d => d.OrderID == orderId);
        foreach (var detail in oldDetails)
        {
            _orderDetailRepository.Remove(detail);
        }
        order.OrderDetails = new List<OrderDetail>();
        foreach (var detailDto in dto.OrderDetails)
        {
            var detail = new OrderDetail
            {
                ProductID = detailDto.ProductID,
                OrderQuantity = detailDto.OrderQuantity,
                ExpectedDate = detailDto.ExpectedDate,
                OrderID = order.OrderID
            };
            await _orderDetailRepository.AddAsync(detail);
            order.OrderDetails.Add(detail);
        }
        await _orderRepository.UpdateAsync(order);
        return order;
    }
    public async Task DeleteOrderAsync(int orderId)
    {
        var order = await _orderRepository.GetByIdAsync(orderId);
        if (order != null)
        {
            var details = await _orderDetailRepository.FindAsync(d => d.OrderID == orderId);
            foreach (var detail in details)
            {
                _orderDetailRepository.Remove(detail);
            }
            _orderRepository.Remove(order);
        }
    }
    public async Task<Order?> GetOrderByIdAsync(int orderId)
    {
        return await _orderRepository.GetByIdAsync(orderId);
    }
    public async Task<IEnumerable<Order>> GetAllOrdersAsync()
    {
        return await _orderRepository.GetAllAsync();
    }
    // CRUD cũ giữ lại cho các trường hợp khác
    public async Task<Order> CreateOrderAsync(Order order)
    {
        await _orderRepository.AddAsync(order);
        return order;
    }
    public async Task<Order> UpdateOrderAsync(Order order)
    {
        await _orderRepository.UpdateAsync(order);
        return order;
    }
} 