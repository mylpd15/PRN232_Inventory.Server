
using Inventory.Domain;

namespace Inventory.Services
{
    public class DeliveryService : IDeliveryService
    {
        private readonly List<DeliveryDto> _deliveries = new();
        private readonly ICustomerService _customerService;
        // Giả lập kiểm tra product và cập nhật tồn kho
        private readonly List<Guid> _validProductIds = new() { Guid.NewGuid(), Guid.NewGuid() };

        public DeliveryService(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task<DeliveryDto> CreateDeliveryAsync(CreateDeliveryDto dto)
        {
            // Validation: Customer phải tồn tại
            var customer = await _customerService.GetCustomerByIdAsync(dto.CustomerId);
            if (customer == null) throw new Exception("Customer does not exist");

            // Validation: DeliveryDetail phải có product hợp lệ và số lượng > 0
            foreach (var detail in dto.DeliveryDetails)
            {
                if (!_validProductIds.Contains(detail.ProductId))
                    throw new Exception($"Product {detail.ProductId} is invalid");
                if (detail.Quantity <= 0)
                    throw new Exception("Quantity must be greater than 0");
            }

            var delivery = new DeliveryDto
            {
                Id = Guid.NewGuid(),
                CustomerId = dto.CustomerId,
                DeliveryDate = dto.DeliveryDate,
                DeliveryStatus = DeliveryStatus.Pending,
                Note = dto.Note,
                Customer = customer,
                DeliveryDetails = dto.DeliveryDetails.Select(d => new DeliveryDetailDto
                {
                    Id = Guid.NewGuid(),
                    ProductId = d.ProductId,
                    Quantity = d.Quantity,
                    UnitPrice = d.UnitPrice
                }).ToList()
            };
            _deliveries.Add(delivery);
            return await Task.FromResult(delivery);
        }

        public async Task<DeliveryDto> UpdateDeliveryAsync(Guid id, UpdateDeliveryDto dto)
        {
            var delivery = _deliveries.FirstOrDefault(x => x.Id == id);
            if (delivery == null) return null;
            // Không cho đổi trạng thái nếu đã delivered/cancelled
            if (delivery.DeliveryStatus == DeliveryStatus.Delivered || delivery.DeliveryStatus == DeliveryStatus.Cancelled)
                throw new Exception("Cannot update a delivered or cancelled delivery");

            // Validation: Customer phải tồn tại nếu đổi customer
            if (dto.CustomerId != Guid.Empty && dto.CustomerId != delivery.CustomerId)
            {
                var customer = await _customerService.GetCustomerByIdAsync(dto.CustomerId);
                if (customer == null) throw new Exception("Customer does not exist");
                delivery.CustomerId = dto.CustomerId;
                delivery.Customer = customer;
            }
            delivery.DeliveryDate = dto.DeliveryDate;
            delivery.DeliveryStatus = dto.DeliveryStatus;
            delivery.Note = dto.Note;
            // DeliveryDetail validation tương tự khi update
            if (dto.DeliveryDetails != null)
            {
                delivery.DeliveryDetails = dto.DeliveryDetails.Select(d => new DeliveryDetailDto
                {
                    Id = d.Id == Guid.Empty ? Guid.NewGuid() : d.Id,
                    ProductId = d.ProductId,
                    Quantity = d.Quantity,
                    UnitPrice = d.UnitPrice
                }).ToList();
                foreach (var detail in delivery.DeliveryDetails)
                {
                    if (!_validProductIds.Contains(detail.ProductId))
                        throw new Exception($"Product {detail.ProductId} is invalid");
                    if (detail.Quantity <= 0)
                        throw new Exception("Quantity must be greater than 0");
                }
            }
            // Trigger cập nhật tồn kho nếu giao thành công
            if (delivery.DeliveryStatus == DeliveryStatus.Delivered)
            {
                // TODO: Gọi hàm cập nhật tồn kho thực tế
            }
            return await Task.FromResult(delivery);
        }

        public async Task<bool> DeleteDeliveryAsync(Guid id)
        {
            var delivery = _deliveries.FirstOrDefault(x => x.Id == id);
            if (delivery == null) return false;
            _deliveries.Remove(delivery);
            return await Task.FromResult(true);
        }

        public async Task<DeliveryDto> GetDeliveryByIdAsync(Guid id)
        {
            var delivery = _deliveries.FirstOrDefault(x => x.Id == id);
            return await Task.FromResult(delivery);
        }

        public async Task<List<DeliveryDto>> GetAllDeliveriesAsync()
        {
            return await Task.FromResult(_deliveries.ToList());
        }
    }
}