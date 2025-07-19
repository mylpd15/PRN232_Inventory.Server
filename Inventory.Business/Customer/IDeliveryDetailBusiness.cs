using WareSync.Domain;

namespace WareSync.Business;
public interface IDeliveryDetailBusiness
{
    Task<DeliveryDetail> CreateDeliveryDetailAsync(DeliveryDetail detail);
    Task<DeliveryDetail> UpdateDeliveryDetailAsync(DeliveryDetail detail);
    Task DeleteDeliveryDetailAsync(int deliveryDetailId);
    Task<DeliveryDetail?> GetDeliveryDetailByIdAsync(int deliveryDetailId);
    Task<IEnumerable<DeliveryDetail>> GetAllDeliveryDetailsAsync();
} 