using WareSync.Domain;

namespace WareSync.Repositories;
public class OrderDetailRepository : GenericRepository<OrderDetail>, IOrderDetailRepository
{
    public OrderDetailRepository(DataContext context) : base(context) { }
    // Thêm các method đặc thù nếu cần
} 