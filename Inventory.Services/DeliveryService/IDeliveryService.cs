using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Inventory.Domain;
using Inventory.Domain.DTOs.DeliveryDto;

namespace Inventory.Services
{
    public interface IDeliveryService
    {
        Task<DeliveryDto> CreateDeliveryAsync(CreateDeliveryDto dto);
        Task<DeliveryDto> UpdateDeliveryAsync(Guid id, UpdateDeliveryDto dto);
        Task<bool> DeleteDeliveryAsync(Guid id);
        Task<DeliveryDto> GetDeliveryByIdAsync(Guid id);
        Task<List<DeliveryDto>> GetAllDeliveriesAsync();
    }
}