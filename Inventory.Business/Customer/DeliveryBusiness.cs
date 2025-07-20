using WareSync.Domain;
using WareSync.Repositories;
using WareSync.Business;

namespace WareSync.Business;
public class DeliveryBusiness : IDeliveryBusiness
{
    private readonly IDeliveryRepository _deliveryRepository;
    private readonly IDeliveryDetailRepository _deliveryDetailRepository;
    public DeliveryBusiness(IDeliveryRepository deliveryRepository, IDeliveryDetailRepository deliveryDetailRepository)
    {
        _deliveryRepository = deliveryRepository;
        _deliveryDetailRepository = deliveryDetailRepository;
    }
    public async Task<Delivery> CreateDeliveryAsync(CreateDeliveryDto dto)
    {
        var delivery = new Delivery
        {
            SalesDate = dto.SalesDate,
            CustomerID = dto.CustomerID,
            DeliveryDetails = new List<DeliveryDetail>()
        };
        await _deliveryRepository.AddAsync(delivery);
        foreach (var detailDto in dto.DeliveryDetails)
        {
            var detail = new DeliveryDetail
            {
                ProductID = detailDto.ProductID,
                DeliveryQuantity = detailDto.DeliveryQuantity,
                ExpectedDate = detailDto.ExpectedDate,
                DeliveryID = delivery.DeliveryID
            };
            await _deliveryDetailRepository.AddAsync(detail);
            delivery.DeliveryDetails.Add(detail);
        }
        return delivery;
    }
    public async Task<Delivery> UpdateDeliveryAsync(int deliveryId, CreateDeliveryDto dto)
    {
        var delivery = await _deliveryRepository.GetByIdAsync(deliveryId);
        if (delivery == null) throw new Exception("Delivery not found");

        delivery.SalesDate = dto.SalesDate;
        delivery.CustomerID = dto.CustomerID;

        // Xóa các DeliveryDetail cũ
        var oldDetails = await _deliveryDetailRepository.FindAsync(d => d.DeliveryID == deliveryId);
        foreach (var detail in oldDetails)
        {
            _deliveryDetailRepository.Remove(detail);
        }

        // Thêm các DeliveryDetail mới
        delivery.DeliveryDetails = new List<DeliveryDetail>();
        foreach (var detailDto in dto.DeliveryDetails)
        {
            var detail = new DeliveryDetail
            {
                ProductID = detailDto.ProductID,
                DeliveryQuantity = detailDto.DeliveryQuantity,
                ExpectedDate = detailDto.ExpectedDate,
                DeliveryID = delivery.DeliveryID
            };
            delivery.DeliveryDetails.Add(detail);
        }

        // ❌ KHÔNG GỌI UpdateAsync vì EF đã tracking
        await _deliveryRepository.SaveChangesAsync(); // ✅ chỉ cần save
        return delivery;
    }

    public async Task DeleteDeliveryAsync(int deliveryId)
    {
        var delivery = await _deliveryRepository.GetByIdAsync(deliveryId);
        if (delivery != null)
        {
            var details = await _deliveryDetailRepository.FindAsync(d => d.DeliveryID == deliveryId);
            foreach (var detail in details)
            {
                _deliveryDetailRepository.Remove(detail);
            }
            _deliveryRepository.Remove(delivery);
        }
    }
    public async Task<Delivery?> GetDeliveryByIdAsync(int deliveryId)
    {
        return await _deliveryRepository.GetByIdAsync(deliveryId);
    }
    public async Task<IEnumerable<Delivery>> GetAllDeliveriesAsync()
    {
        return await _deliveryRepository.GetAllDeliveriesWithDetailsAsync();
    }

    public async Task<Delivery> CreateDeliveryAsync(Delivery delivery)
    {
        await _deliveryRepository.AddAsync(delivery);
        return delivery;
    }
    public async Task<Delivery> UpdateDeliveryAsync(Delivery delivery)
    {
        // Check if delivery exists
        if (!await DeliveryExistsAsync(delivery.DeliveryID))
        {
            throw new InvalidOperationException($"Delivery with ID {delivery.DeliveryID} does not exist.");
        }

        await _deliveryRepository.UpdateAsync(delivery);
        return delivery;
    }

    public async Task<bool> DeliveryExistsAsync(int deliveryId)
    {
        return await _deliveryRepository.GetByIdAsync(deliveryId) != null;
    }

    public async Task<IEnumerable<Delivery>> GetDeliveriesByCustomerAsync(int customerId)
    {
        return await _deliveryRepository.FindAsync(d => d.CustomerID == customerId);
    }
} 