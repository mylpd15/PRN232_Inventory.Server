namespace WareSync.Api.DTOs;
public class DeliveryDetailDto
{
    public int DeliveryDetailID { get; set; }
    public int DeliveryID { get; set; }
    public int ProductID { get; set; }
    public string? ProductName { get; set; }
    public int DeliveryQuantity { get; set; }
    public DateTime ExpectedDate { get; set; }
    public DateTime? ActualDate { get; set; }
} 