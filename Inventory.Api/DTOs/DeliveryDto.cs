namespace WareSync.Api.DTOs;
public class DeliveryDto
{
    public int DeliveryID { get; set; }
    public DateTime SalesDate { get; set; }
    public int CustomerID { get; set; }
    public string? CustomerName { get; set; }
    public List<DeliveryDetailDto> DeliveryDetails { get; set; } = new();
}
