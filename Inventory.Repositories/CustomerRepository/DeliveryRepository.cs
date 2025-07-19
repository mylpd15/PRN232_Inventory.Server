using WareSync.Domain;

namespace WareSync.Repositories;
public class DeliveryRepository : GenericRepository<Delivery>, IDeliveryRepository
{
    public DeliveryRepository(DataContext context) : base(context) { }
    // Thêm các method đặc thù nếu cần
} 