namespace WareSync.Api.DTOs;
public class DeliveryDetailCreateDto
{
    public int DeliveryID { get; set; }
    public int ProductID { get; set; }
    public int DeliveryQuantity { get; set; }
    public DateTime ExpectedDate { get; set; }
} 