using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WareSync.Domain;

namespace WareSync.Repositories
{
    public class InventoryLogRepository : GenericRepository<InventoryLog>, IInventoryLogRepository
    {
        public InventoryLogRepository(DataContext context) : base(context) { }
    }
}
