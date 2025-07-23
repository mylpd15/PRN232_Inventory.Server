using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WareSync.Domain;
using WareSync.Repositories;

namespace WareSync.Business
{
    public class InventoryLogBusiness : IInventoryLogBusiness
    {
        private readonly IInventoryLogRepository _inventoryLogRepository;
        public InventoryLogBusiness(IInventoryLogRepository inventoryLogRepository)
        {
            _inventoryLogRepository = inventoryLogRepository;
        }

        public async Task<IEnumerable<InventoryLog>> GetInventoryLogsAsync()
        {
            return await _inventoryLogRepository.GetAllAsync();
        }
        public async Task<InventoryLog?> GetInventoryLogByIdAsync(int inventoryLogId)
        {
            return await _inventoryLogRepository.GetByIdAsync(inventoryLogId);
        }
        public async Task<InventoryLog> CreateInventoryLogAsync(InventoryLog inventoryLog)
        {
            await _inventoryLogRepository.AddAsync(inventoryLog);
            return inventoryLog;
        }
    }
}
