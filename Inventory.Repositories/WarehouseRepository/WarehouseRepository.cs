using WareSync.Domain;

namespace WareSync.Repositories.WarehouseRepository;
public class WarehouseRepository : GenericRepository<Warehouse>, IWarehouseRepository
{
    public WarehouseRepository(DataContext context) : base(context) { }
    // Thêm các method đặc thù nếu cần
} 