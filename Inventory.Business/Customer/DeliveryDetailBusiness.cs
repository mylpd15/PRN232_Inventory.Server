using WareSync.Repositories;
using WareSync.Domain;

namespace WareSync.Business;
public class DeliveryDetailBusiness : IDeliveryDetailBusiness
{
    private readonly IDeliveryDetailRepository _deliveryDetailRepository;

    public DeliveryDetailBusiness(IDeliveryDetailRepository deliveryDetailRepository)
    {
        _deliveryDetailRepository = deliveryDetailRepository;
    }

    public async Task<DeliveryDetail> CreateDeliveryDetailAsync(DeliveryDetail detail)
    {
        return await _deliveryDetailRepository.AddAsync(detail);
    }

    public async Task<DeliveryDetail> UpdateDeliveryDetailAsync(DeliveryDetail detail)
    {
        return await _deliveryDetailRepository.UpdateAsync(detail);
    }

    public async Task DeleteDeliveryDetailAsync(int deliveryDetailId)
    {
        var detail = await _deliveryDetailRepository.GetByIdAsync(deliveryDetailId);
        if (detail != null)
        {
            _deliveryDetailRepository.Remove(detail);
        }
    }

    public async Task<DeliveryDetail?> GetDeliveryDetailByIdAsync(int deliveryDetailId)
    {
        return await _deliveryDetailRepository.GetByIdAsync(deliveryDetailId);
    }

    public async Task<IEnumerable<DeliveryDetail>> GetAllDeliveryDetailsAsync()
    {
        return await _deliveryDetailRepository.GetAllAsync();
    }
} 