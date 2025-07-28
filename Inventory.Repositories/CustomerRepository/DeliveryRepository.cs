using Microsoft.EntityFrameworkCore;
using WareSync.Domain;

namespace WareSync.Repositories;
public class DeliveryRepository : GenericRepository<Delivery>, IDeliveryRepository
{
    private readonly DataContext _context;
    public DeliveryRepository(DataContext context) : base(context) { _context = context; }

    public async Task AddDeliveryWithDetailsAsync(Delivery delivery)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            _context.Deliveries.Add(delivery);
            await _context.SaveChangesAsync(); 

            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }


    public IQueryable<Delivery> GetAllDeliveriesWithDetails()
    {
        return _context.Deliveries
            .Include(d => d.Customer)
            .Include(d => d.DeliveryDetails)
                .ThenInclude(dd => dd.Product);
                    //.ThenInclude(p => p.Prices);
    }


    // Thêm các method đặc thù nếu cần
    public async Task<IEnumerable<Delivery>> GetAllDeliveriesWithDetailsAsync()
    {
        return await _context.Deliveries
            .Include(d => d.Customer)
            .Include(d => d.DeliveryDetails)
            .ThenInclude(dd => dd.Product)
            .ToListAsync();
    }

public async Task<Delivery> GetByDeliveryIdAsync(int deliveryId)
{
    return await _context.Deliveries
        .Include(d => d.Customer)
        .Include(d => d.DeliveryDetails)
            .ThenInclude(dd => dd.Product)
            .ThenInclude(p => p.Prices)
        .FirstOrDefaultAsync(d => d.DeliveryID == deliveryId);
}


    public async Task<Delivery> UpdateAsync(Delivery delivery)
    {
        var existing = await _context.Deliveries
            .FirstOrDefaultAsync(d => d.DeliveryID == delivery.DeliveryID);

        if (existing == null)
            throw new Exception($"Delivery with ID {delivery.DeliveryID} not found.");

        // Chỉ cập nhật các thuộc tính của Delivery
        existing.SalesDate = delivery.SalesDate;
        existing.CustomerID = delivery.CustomerID;

        await _context.SaveChangesAsync();
        return existing;
    }


}