using WareSync.Domain;

namespace WareSync.Repositories;
public class OrderRepository : GenericRepository<Order>, IOrderRepository
{
    public OrderRepository(DataContext context) : base(context) { }
    // Thêm các method đặc thù nếu cần
} 