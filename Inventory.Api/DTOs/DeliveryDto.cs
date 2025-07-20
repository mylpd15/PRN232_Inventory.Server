using WareSync.Domain.Enums;

namespace WareSync.Api.DTOs;
public class DeliveryDto
{
    public int DeliveryID { get; set; }
    public DateTime SalesDate { get; set; }
    public int CustomerID { get; set; }
    public List<DeliveryDetailDto> DeliveryDetails { get; set; } = new();
    public DeliveryStatus Status { get; set; }
}
