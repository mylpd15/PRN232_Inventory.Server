using WareSync.Domain;

namespace WareSync.Business;
public interface ITransferBusiness
{
    Task<Transfer> CreateTransferAsync(Transfer transfer);
    Task<Transfer> UpdateTransferAsync(Transfer transfer);
    Task DeleteTransferAsync(int transferId);
    Task<Transfer?> GetTransferByIdAsync(int transferId);
    Task<IEnumerable<Transfer>> GetAllTransfersAsync();
} 