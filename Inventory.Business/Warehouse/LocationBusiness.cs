using WareSync.Domain;
using WareSync.Repositories;

namespace WareSync.Business;
public class LocationBusiness : ILocationBusiness
{
    private readonly ILocationRepository _locationRepository;
    public LocationBusiness(ILocationRepository locationRepository)
    {
        _locationRepository = locationRepository;
    }
    public async Task<Location> CreateLocationAsync(Location location)
    {
        await _locationRepository.AddAsync(location);
        return location;
    }
    public async Task<Location> UpdateLocationAsync(Location location)
    {
        await _locationRepository.UpdateAsync(location);
        return location;
    }
    public async Task DeleteLocationAsync(int locationId)
    {
        var location = await _locationRepository.GetByIdAsync(locationId);
        if (location != null)
           await _locationRepository.Remove(location);
    }
    public async Task<Location?> GetLocationByIdAsync(int locationId)
    {
        return await _locationRepository.GetByIdAsync(locationId);
    }
    public async Task<IEnumerable<Location>> GetAllLocationsAsync()
    {
        return await _locationRepository.GetAllAsync();
    }
} 