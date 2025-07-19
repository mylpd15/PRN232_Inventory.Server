namespace WareSync.Api.DTOs;
public class OrderDto
{
    public int OrderID { get; set; }
    public DateTime OrderDate { get; set; }
    public int ProviderID { get; set; }
    public string? ProviderName { get; set; }
    public List<OrderDetailDto> OrderDetails { get; set; } = new();
}