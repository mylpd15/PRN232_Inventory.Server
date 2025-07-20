using WareSync.Domain;

namespace WareSync.Repositories.ProviderRepository;
public class ProviderRepository : GenericRepository<Provider>, IProviderRepository
{
    public ProviderRepository(DataContext context) : base(context) { }
    // Thêm các method đặc thù nếu cần
} 