using WareSync.Repositories;
using WareSync.Domain;
using WareSync.Domain.Enums;

namespace WareSync.Business;
public class DeliveryDetailBusiness : IDeliveryDetailBusiness
{
    private readonly IDeliveryDetailRepository _deliveryDetailRepository;
    private readonly IProductBusiness _productBusiness;
    private readonly IInventoryBusiness _inventoryBusiness;
    private readonly IDeliveryRepository _deliveryRepository;

    public DeliveryDetailBusiness(IDeliveryDetailRepository deliveryDetailRepository, IProductBusiness productBusiness, IInventoryBusiness inventoryBusiness, IDeliveryRepository deliveryRepository)
    {
        _deliveryDetailRepository = deliveryDetailRepository;
        _productBusiness = productBusiness;
        _inventoryBusiness = inventoryBusiness;
        _deliveryRepository = deliveryRepository;
    }

    public async Task<DeliveryDetail> CreateDeliveryDetailAsync(DeliveryDetail detail)
    {
        // Check parent delivery status
        var delivery = await _deliveryRepository.GetByIdAsync(detail.DeliveryID);
        if (delivery == null)
            throw new InvalidOperationException($"Delivery with ID {detail.DeliveryID} does not exist.");
        if (delivery.Status != DeliveryStatus.Pending)
            throw new InvalidOperationException("Can only add delivery detail if delivery status is Pending.");
        if (detail.ExpectedDate <= DateTime.UtcNow)
            throw new InvalidOperationException("ExpectedDate must be in the future.");
        // Transaction start
        try
        {
                // Check if a detail with the same product already exists in this delivery
            var existingDetail = (await _deliveryDetailRepository.FindAsync(d => d.DeliveryID == detail.DeliveryID && d.ProductID == detail.ProductID)).FirstOrDefault();
            if (existingDetail != null)
            {
                // Update the quantity instead of creating a new record
                var newQuantity = existingDetail.DeliveryQuantity + detail.DeliveryQuantity;
                // Check product existence
                var product = await _productBusiness.GetProductByIdAsync(detail.ProductID);
                if (product == null)
                    throw new InvalidOperationException($"Product with ID {detail.ProductID} does not exist.");
                // Check inventory
                var inventory = (await _inventoryBusiness.GetAllInventoriesAsync()).FirstOrDefault(i => i.ProductID == detail.ProductID);
                if (inventory == null || inventory.QuantityAvailable < detail.DeliveryQuantity)
                    throw new InvalidOperationException($"Not enough inventory for product {detail.ProductID}.");
                // Subtract inventory for the new quantity being added
                inventory.QuantityAvailable -= detail.DeliveryQuantity;
                await _inventoryBusiness.UpdateInventoryAsync(inventory);
                existingDetail.DeliveryQuantity = newQuantity;
                await _deliveryDetailRepository.UpdateAsync(existingDetail);
                return existingDetail;
            }
            else
            {
                // Check product existence
                var product = await _productBusiness.GetProductByIdAsync(detail.ProductID);
                if (product == null)
                    throw new InvalidOperationException($"Product with ID {detail.ProductID} does not exist.");
                // Check inventory
                var inventory = (await _inventoryBusiness.GetAllInventoriesAsync()).FirstOrDefault(i => i.ProductID == detail.ProductID);
                if (inventory == null || inventory.QuantityAvailable < detail.DeliveryQuantity)
                    throw new InvalidOperationException($"Not enough inventory for product {detail.ProductID}.");
                // Subtract inventory
                inventory.QuantityAvailable -= detail.DeliveryQuantity;
                await _inventoryBusiness.UpdateInventoryAsync(inventory);
                return await _deliveryDetailRepository.AddAsync(detail);
            }
        }
        catch
        {
            // Rollback logic if needed
            throw;
        }
    }

    public async Task<DeliveryDetail> UpdateDeliveryDetailAsync(DeliveryDetail detail)
    {
        var existing = await _deliveryDetailRepository.GetByIdAsync(detail.DeliveryDetailID);
        if (existing == null)
            throw new InvalidOperationException($"DeliveryDetail with ID {detail.DeliveryDetailID} does not exist.");
        // Check parent delivery status
        var delivery = await _deliveryRepository.GetByIdAsync(detail.DeliveryID);
        if (delivery == null)
            throw new InvalidOperationException($"Delivery with ID {detail.DeliveryID} does not exist.");
        if (delivery.Status != DeliveryStatus.Pending)
            throw new InvalidOperationException("Can only update delivery detail if delivery status is Pending.");
        // Transaction start
        try
        {
            // Check product existence
            var product = await _productBusiness.GetProductByIdAsync(detail.ProductID);
            if (product == null)
                throw new InvalidOperationException($"Product with ID {detail.ProductID} does not exist.");
            var inventory = (await _inventoryBusiness.GetAllInventoriesAsync()).FirstOrDefault(i => i.ProductID == detail.ProductID);
            if (inventory == null)
                throw new InvalidOperationException($"No inventory found for product {detail.ProductID}.");
            if (detail.DeliveryQuantity > existing.DeliveryQuantity)
            {
                // Increase: check and subtract difference
                int diff = detail.DeliveryQuantity - existing.DeliveryQuantity;
                if (inventory.QuantityAvailable < diff)
                    throw new InvalidOperationException($"Not enough inventory for product {detail.ProductID}.");
                inventory.QuantityAvailable -= diff;
                await _inventoryBusiness.UpdateInventoryAsync(inventory);
            }
            else if (detail.DeliveryQuantity < existing.DeliveryQuantity)
            {
                // Decrease: add back difference
                int diff = existing.DeliveryQuantity - detail.DeliveryQuantity;
                inventory.QuantityAvailable += diff;
                await _inventoryBusiness.UpdateInventoryAsync(inventory);
            }
            //return await _deliveryDetailRepository.UpdateAsync(detail);
            // Only update the tracked entity
            if (detail.ExpectedDate <= DateTime.UtcNow)
                throw new InvalidOperationException("ExpectedDate must be in the future.");
            existing.DeliveryQuantity = detail.DeliveryQuantity;
            existing.ExpectedDate = detail.ExpectedDate;
            existing.ActualDate = detail.ActualDate;
            // ... update other fields if needed ...
            await _deliveryDetailRepository.UpdateAsync(existing);
            return existing;
        }
        catch
        {
            // Rollback logic if needed
            throw;
        }
    }

    public async Task DeleteDeliveryDetailAsync(int deliveryDetailId)
    {
        var detail = await _deliveryDetailRepository.GetByIdAsync(deliveryDetailId);
        if (detail != null)
        {
            // Check parent delivery status
            var delivery = await _deliveryRepository.GetByIdAsync(detail.DeliveryID);
            if (delivery == null)
                throw new InvalidOperationException($"Delivery with ID {detail.DeliveryID} does not exist.");
            if (delivery.Status != DeliveryStatus.Pending)
                throw new InvalidOperationException("Can only delete delivery detail if delivery status is Pending.");
            // Transaction start
            try
            {
                var inventory = (await _inventoryBusiness.GetAllInventoriesAsync()).FirstOrDefault(i => i.ProductID == detail.ProductID);
                if (inventory != null)
                {
                    inventory.QuantityAvailable += detail.DeliveryQuantity;
                    await _inventoryBusiness.UpdateInventoryAsync(inventory);
                }
                _deliveryDetailRepository.Remove(detail);
            }
            catch
            {
                // Rollback logic if needed
                throw;
            }
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