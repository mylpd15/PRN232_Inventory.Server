using WareSync.Domain;

namespace WareSync.Business;
public interface ILocationBusiness
{
    Task<Location> CreateLocationAsync(Location location);
    Task<Location> UpdateLocationAsync(Location location);
    Task DeleteLocationAsync(int locationId);
    Task<Location?> GetLocationByIdAsync(int locationId);
    Task<IEnumerable<Location>> GetAllLocationsAsync();
} 