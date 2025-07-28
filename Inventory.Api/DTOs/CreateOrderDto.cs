namespace WareSync.Api.DTOs;

public class CreateOrderDto
{
    public DateTime OrderDate { get; set; }
    public int ProviderID { get; set; }
    public int WarehouseId { get; set; } // Added for warehouse reference
    public List<CreateOrderDetailDto> OrderDetails { get; set; } = new();
}

public class CreateOrderDetailDto
{
    public int ProductID { get; set; }
    public int OrderQuantity { get; set; }
    public DateTime ExpectedDate { get; set; }
} 