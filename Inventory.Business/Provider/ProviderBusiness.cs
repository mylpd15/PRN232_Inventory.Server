using WareSync.Domain;
using WareSync.Repositories.ProviderRepository;

namespace WareSync.Business;

public class ProviderBusiness : IProviderBusiness
{
    private readonly IProviderRepository _providerRepository;

    public ProviderBusiness(IProviderRepository providerRepository)
    {
        _providerRepository = providerRepository;
    }

    public async Task<Provider> CreateProviderAsync(Provider provider)
    {
        return await _providerRepository.AddAsync(provider);
    }

    public async Task DeleteProviderAsync(int providerId)
    {
        var provider = await _providerRepository.GetByIdAsync(providerId);
        if (provider == null)
            throw new Exception($"Provider with ID {providerId} not found.");
        
        _providerRepository.Remove(provider);
        await _providerRepository.SaveChangesAsync();
    }

    public async Task<IEnumerable<Provider>> GetAllProvidersAsync()
    {
        return await _providerRepository.GetAllAsync();
    }

    public async Task<Provider?> GetProviderByIdAsync(int providerId)
    {
        return await _providerRepository.GetByIdAsync(providerId);
    }

    public async Task<Provider> UpdateProviderAsync(Provider provider)
    {
        var existingProvider = await _providerRepository.GetByIdAsync(provider.ProviderID);
        if (existingProvider == null)
            throw new Exception($"Provider with ID {provider.ProviderID} not found.");
        
        return await _providerRepository.UpdateAsync(provider);
    }
} 