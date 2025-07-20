using WareSync.Domain;

namespace WareSync.Repositories.WarehouseRepository;
public class LocationRepository : GenericRepository<Location>, ILocationRepository
{
    public LocationRepository(DataContext context) : base(context) { }
    // Thêm các method đặc thù nếu cần
} 