using WareSync.Domain;
using WareSync.Repositories;
using WareSync.Business;
using WareSync.Domain.Enums;
using System.Diagnostics;

namespace WareSync.Business;
public class OrderBusiness : IOrderBusiness
{
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderDetailRepository _orderDetailRepository;
    private readonly IInventoryBusiness _inventoryBusiness;
    public OrderBusiness(IOrderRepository orderRepository, IOrderDetailRepository orderDetailRepository, IInventoryBusiness inventoryBusiness)
    {
        _orderRepository = orderRepository;
        _orderDetailRepository = orderDetailRepository;
        _inventoryBusiness = inventoryBusiness;
    }
    public async Task<Order> CreateOrderAsync(CreateOrderDto dto)
    {
        Debug.WriteLine($"Creating order now.......");
        var order = new Order
        {
            OrderDate = dto.OrderDate,
            ProviderID = dto.ProviderID,
            WarehouseID = dto.WarehouseId, // Set warehouse
            Status = OrderStatus.Pending, // Always set to Pending on creation
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
        await _orderRepository.SaveChangesAsync();
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
            await _orderRepository.SaveChangesAsync();
        }
    }
    public async Task<Order?> GetOrderByIdAsync(int orderId)
    {
        return await _orderRepository.GetByIdWithDetailsAsync(orderId);
    }
    public async Task<IEnumerable<Order>> GetAllOrdersAsync()
    {
        return await _orderRepository.GetAllOrdersWithProviderAndWarehouse();
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
        await _orderRepository.SaveChangesAsync();
        return order;
    }
    public async Task<Order> UpdateOrderStatusAsync(int orderId, OrderStatus newStatus, string? rejectReason = null)
    {
        var order = await _orderRepository.GetByIdWithDetailsAsync(orderId);
        if (order == null) throw new Exception("Order not found");
        
        Debug.WriteLine($"UpdateOrderStatusAsync called - OrderID: {orderId}, NewStatus: {newStatus}, CurrentStatus: {order.Status}");
        
        // Store the original status before updating it
        var originalStatus = order.Status;
        
        order.Status = newStatus;
        if (newStatus == OrderStatus.Rejected)
        {
            order.RejectReason = rejectReason;
        }
        else
        {
            order.RejectReason = null;
        }
        
        Debug.WriteLine($"OrderDetails is null: {order.OrderDetails == null}");
        if (order.OrderDetails != null)
        {
            Debug.WriteLine($"OrderDetails count: {order.OrderDetails.Count}");
        }
        
        // Prevent double-completion: only update inventory if previous status is not Completed
        if (newStatus == OrderStatus.Completed && order.OrderDetails != null && originalStatus != OrderStatus.Completed)
        {
            Debug.WriteLine("Entering inventory update block");
            foreach (var detail in order.OrderDetails)
            {
                var inventory = await _inventoryBusiness.GetByProductAndWarehouseAsync(detail.ProductID, order.WarehouseID);
                if (inventory != null)
                {
                    inventory.QuantityAvailable += detail.OrderQuantity;
                    await _inventoryBusiness.UpdateInventoryAsync(inventory);
                    Debug.WriteLine($" Inventory ID: {inventory.InventoryID}; Quantity available: {inventory.QuantityAvailable}; Order Quantity: {detail.OrderQuantity}");
                }
                else
                {
                    
                    Debug.WriteLine($"No inventory found for ProductID {detail.ProductID} in WarehouseID {order.WarehouseID}");
                }
            }
        }
        else
        {
            Debug.WriteLine($"Not updating inventory - newStatus: {newStatus}, OrderDetails null: {order.OrderDetails == null}, Original status: {originalStatus}");
        }
        
        await _orderRepository.UpdateAsync(order);
        await _orderRepository.SaveChangesAsync();
        return order;
    }
} 