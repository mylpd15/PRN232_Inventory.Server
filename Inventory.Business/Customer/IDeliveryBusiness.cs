using WareSync.Domain;

namespace WareSync.Business;
public interface IDeliveryBusiness
{
    Task<Delivery> CreateDeliveryAsync(Delivery delivery);
    Task<Delivery> UpdateDeliveryAsync(Delivery delivery);
    Task DeleteDeliveryAsync(int deliveryId);
    Task<Delivery?> GetDeliveryByIdAsync(int deliveryId);
    Task<IEnumerable<Delivery>> GetAllDeliveriesAsync();
} 