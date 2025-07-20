using WareSync.Domain;

namespace WareSync.Repositories;
public interface IDeliveryRepository : IGenericRepository<Delivery>
{
    // Thêm các method đặc thù nếu cần
    Task<IEnumerable<Delivery>> GetAllDeliveriesWithDetailsAsync();
    Task<Delivery> UpdateAsync(Delivery delivery);
} 