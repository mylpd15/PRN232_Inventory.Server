using WareSync.Domain;
using Microsoft.EntityFrameworkCore;

namespace WareSync.Repositories;
public class OrderRepository : GenericRepository<Order>, IOrderRepository
{
    public OrderRepository(DataContext context) : base(context) { }
    
    public async Task<Order?> GetByIdWithDetailsAsync(int orderId)
    {
        var order = await _context.Orders
            .Include(o => o.OrderDetails)
            .FirstOrDefaultAsync(o => o.OrderID == orderId);
            
        System.Diagnostics.Debug.WriteLine($"GetByIdWithDetailsAsync - OrderID: {orderId}, Order found: {order != null}");
        if (order != null)
        {
            System.Diagnostics.Debug.WriteLine($"OrderDetails is null: {order.OrderDetails == null}");
            if (order.OrderDetails != null)
            {
                System.Diagnostics.Debug.WriteLine($"OrderDetails count: {order.OrderDetails.Count}");
            }
            
            // Check if there are any order details in the database for this order
            var orderDetailsCount = await _context.OrderDetails.CountAsync(od => od.OrderID == orderId);
            System.Diagnostics.Debug.WriteLine($"OrderDetails count in database for OrderID {orderId}: {orderDetailsCount}");
        }
        
        return order;
    }
} 