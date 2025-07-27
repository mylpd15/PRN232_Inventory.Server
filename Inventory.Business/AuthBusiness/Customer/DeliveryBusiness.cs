using WareSync.Domain;
using WareSync.Repositories;
using WareSync.Domain.Enums;
using WareSync.Business;

namespace WareSync.Business;
public class DeliveryBusiness : IDeliveryBusiness
{
    private readonly IDeliveryRepository _deliveryRepository;
    private readonly IDeliveryDetailRepository _deliveryDetailRepository;
    private readonly IProductBusiness _productBusiness;
    private readonly IInventoryBusiness _inventoryBusiness;
    public DeliveryBusiness(IDeliveryRepository deliveryRepository, IDeliveryDetailRepository deliveryDetailRepository, IProductBusiness productBusiness, IInventoryBusiness inventoryBusiness)
    {
        _deliveryRepository = deliveryRepository;
        _deliveryDetailRepository = deliveryDetailRepository;
        _productBusiness = productBusiness;
        _inventoryBusiness = inventoryBusiness;
    }
    public async Task<Delivery> CreateDeliveryAsync(CreateDeliveryDto dto)
    {
        if (dto.DeliveryDetails == null || !dto.DeliveryDetails.Any())
            throw new InvalidOperationException("Delivery details must not be empty.");

        var validatedDetails = new List<DeliveryDetail>();

        // Validate all details first
        foreach (var detailDto in dto.DeliveryDetails)
        {
            var product = await _productBusiness.GetProductByIdAsync(detailDto.ProductID);
            if (product == null)
                throw new InvalidOperationException($"Product with ID {detailDto.ProductID} does not exist.");

            var inventory = (await _inventoryBusiness.GetAllInventoriesAsync())
                .FirstOrDefault(i => i.ProductID == detailDto.ProductID);

            if (inventory == null || inventory.QuantityAvailable < detailDto.DeliveryQuantity)
                throw new InvalidOperationException($"Not enough inventory for product {detailDto.ProductID}.");

            validatedDetails.Add(new DeliveryDetail
            {
                ProductID = detailDto.ProductID,
                DeliveryQuantity = detailDto.DeliveryQuantity,
                ExpectedDate = detailDto.ExpectedDate
            });
        }

        var delivery = new Delivery
        {
            SalesDate = dto.SalesDate,
            CustomerID = dto.CustomerID,
            DeliveryDetails = validatedDetails 
        };

        // Let repository or service layer handle saving everything with transaction
        await _deliveryRepository.AddDeliveryWithDetailsAsync(delivery);

        // Update inventory after successful save
        foreach (var detail in validatedDetails)
        {
            var inventory = (await _inventoryBusiness.GetAllInventoriesAsync())
                .FirstOrDefault(i => i.ProductID == detail.ProductID);

            inventory.QuantityAvailable -= detail.DeliveryQuantity;
            await _inventoryBusiness.UpdateInventoryAsync(inventory);
        }

        return delivery;
    }



    public async Task DeleteDeliveryAsync(int deliveryId)
    {
        throw new InvalidOperationException("Deleting deliveries is not allowed. Set status to Cancelled instead.");
    }
    public async Task<Delivery?> GetDeliveryByIdAsync(int deliveryId)
    {
        return await _deliveryRepository.GetByDeliveryIdAsync(deliveryId);
    }
    public IQueryable<Delivery> GetAllDeliveriesAsync()
    {
        return  _deliveryRepository.GetAllDeliveriesWithDetails();
    }

    public async Task<Delivery> CreateDeliveryAsync(Delivery delivery)
    {
        await _deliveryRepository.AddAsync(delivery);
        return delivery;
    }
    public async Task<Delivery> UpdateDeliveryAsync(UpdateDeliveryDto delivery, int deliveryId)
    {
        var existing = await _deliveryRepository.GetByIdAsync(deliveryId);
        if (existing == null)
            throw new InvalidOperationException($"Delivery with ID {deliveryId} does not exist.");
        // Restrict updates if status is Delivered or Shipped
        //if (existing.Status == DeliveryStatus.Delivered)
          //  throw new InvalidOperationException("Cannot update delivery or details when status is Shipped.");
        // If status is being set to Cancelled, add back inventory
        if (delivery.Status == DeliveryStatus.Cancelled && existing.Status != DeliveryStatus.Cancelled)
        {
            var details = await _deliveryDetailRepository.FindAsync(d => d.DeliveryID == deliveryId);
            foreach (var detail in details)
            {
                var inventory = (await _inventoryBusiness.GetAllInventoriesAsync()).FirstOrDefault(i => i.ProductID == detail.ProductID);
                if (inventory != null)
                {
                    inventory.QuantityAvailable += detail.DeliveryQuantity;
                    await _inventoryBusiness.UpdateInventoryAsync(inventory);
                }
            }
        }
        existing.CustomerID = delivery.CustomerID;
        existing.SalesDate = delivery.SalesDate;
        existing.Status = delivery.Status;

        await _deliveryRepository.UpdateAsync(existing);
        return existing;
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