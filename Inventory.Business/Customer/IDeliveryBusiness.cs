using WareSync.Domain;

namespace WareSync.Business;
public interface IDeliveryBusiness
{
    Task<Delivery> CreateDeliveryAsync(CreateDeliveryDto delivery);
    Task<Delivery> UpdateDeliveryAsync(UpdateDeliveryDto delivery, int deliveryId);
    Task DeleteDeliveryAsync(int deliveryId);
    Task<Delivery?> GetDeliveryByIdAsync(int deliveryId);
    IQueryable<Delivery> GetAllDeliveriesAsync();
    Task<bool> DeliveryExistsAsync(int deliveryId);
    Task<IEnumerable<Delivery>> GetDeliveriesByCustomerAsync(int customerId);
} 