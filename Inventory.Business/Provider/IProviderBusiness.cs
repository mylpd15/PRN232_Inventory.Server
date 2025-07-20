using WareSync.Domain;

namespace WareSync.Business;
public interface IProviderBusiness
{
    Task<Domain.Provider> CreateProviderAsync(Domain.Provider provider);
    Task<Domain.Provider> UpdateProviderAsync(Domain.Provider provider);
    Task DeleteProviderAsync(int providerId);
    Task<Domain.Provider?> GetProviderByIdAsync(int providerId);
    Task<IEnumerable<Domain.Provider>> GetAllProvidersAsync();
} 