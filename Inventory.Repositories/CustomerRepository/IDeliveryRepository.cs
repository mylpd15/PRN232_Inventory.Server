using WareSync.Domain;

namespace WareSync.Repositories;
public interface IDeliveryRepository : IGenericRepository<Delivery>
{
    Task<IEnumerable<Delivery>> GetAllDeliveriesWithDetailsAsync();
    IQueryable<Delivery> GetAllDeliveriesWithDetails();

    Task<Delivery> GetByDeliveryIdAsync(int deliveryId);
    Task<Delivery> UpdateAsync(Delivery delivery);

    Task AddDeliveryWithDetailsAsync(Delivery delivery);
} 