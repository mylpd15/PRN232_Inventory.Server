namespace WareSync.Business;
public class CreateDeliveryDto
{
    public DateTime SalesDate { get; set; }
    public int CustomerID { get; set; }
    public List<CreateDeliveryDetailDto> DeliveryDetails { get; set; } = new();
}
public class CreateDeliveryDetailDto
{
    public int ProductID { get; set; }
    public int DeliveryQuantity { get; set; }
    public DateTime ExpectedDate { get; set; }
} 