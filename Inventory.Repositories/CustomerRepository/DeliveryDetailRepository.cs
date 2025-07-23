using WareSync.Domain;

namespace WareSync.Repositories;
public class DeliveryDetailRepository : GenericRepository<DeliveryDetail>, IDeliveryDetailRepository
{
    public DeliveryDetailRepository(DataContext context) : base(context) { }
    // Thêm các method đặc thù nếu cần
} 