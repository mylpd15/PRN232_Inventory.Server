using WareSync.Domain;
using WareSync.Repositories;

namespace WareSync.Business;
public class TransferBusiness : ITransferBusiness
{
    private readonly ITransferRepository _transferRepository;
    public TransferBusiness(ITransferRepository transferRepository)
    {
        _transferRepository = transferRepository;
    }
    public async Task<Transfer> CreateTransferAsync(Transfer transfer)
    {
        await _transferRepository.AddAsync(transfer);
        return transfer;
    }
    public async Task<Transfer> UpdateTransferAsync(Transfer transfer)
    {
        await _transferRepository.UpdateAsync(transfer);
        await _transferRepository.SaveChangesAsync();
        return transfer;
    }
    public async Task DeleteTransferAsync(int transferId)
    {
        var transfer = await _transferRepository.GetByIdAsync(transferId);
        if (transfer != null)
        {
            await _transferRepository.Remove(transfer);
            await _transferRepository.SaveChangesAsync();
        }
    }
    public async Task<Transfer?> GetTransferByIdAsync(int transferId)
    {
        return await _transferRepository.GetByIdAsync(transferId);
    }
    public async Task<IEnumerable<Transfer>> GetAllTransfersAsync()
    {
        return await _transferRepository.GetAllAsync();
    }
} 