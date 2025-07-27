using System.Text.Json.Serialization;

namespace WareSync.Business;
public class CreateDeliveryDto
{
    public DateTime SalesDate { get; set; }
    public int CustomerID { get; set; }
    public List<CreateDeliveryDetailDto> DeliveryDetails { get; set; } = new();
}