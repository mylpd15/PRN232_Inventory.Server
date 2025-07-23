using Microsoft.EntityFrameworkCore;
using WareSync.Domain;

namespace WareSync.Repositories.InventoryRepository;
public class InventoryRepository : GenericRepository<Inventory>, IInventoryRepository
{
   
    public InventoryRepository(DataContext context) : base(context) { }

   
} 