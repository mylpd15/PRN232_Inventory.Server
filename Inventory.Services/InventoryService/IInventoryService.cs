using Inventory.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Services.InventoryService
{
    public interface IInventoryService
    {
        Task<InventoryEntryDto> CreateEntryAsync(CreateInventoryEntryDto dto);
        Task<PageDto<InventoryEntryDto>> GetHistoryAsync(Guid productId, int page, int take);
    }
}
