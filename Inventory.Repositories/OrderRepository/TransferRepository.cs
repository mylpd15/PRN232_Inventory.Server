using WareSync.Domain;

namespace WareSync.Repositories;
public class TransferRepository : GenericRepository<Transfer>, ITransferRepository
{
    public TransferRepository(DataContext context) : base(context) { }
    // Thêm các method đặc thù nếu cần
} 