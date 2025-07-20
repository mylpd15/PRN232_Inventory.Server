using Microsoft.EntityFrameworkCore;
using WareSync.Domain;

namespace WareSync.Repositories;
public class DeliveryRepository : GenericRepository<Delivery>, IDeliveryRepository
{
    private readonly DataContext _context;
    public DeliveryRepository(DataContext context) : base(context) { _context = context; }
    // Thêm các method đặc thù nếu cần
    public async Task<IEnumerable<Delivery>> GetAllDeliveriesWithDetailsAsync()
    {
        return await _context.Deliveries
            .Include(d => d.Customer)
            .Include(d => d.DeliveryDetails)
            .ThenInclude(dd => dd.Product)
            .ToListAsync();
    }
} 