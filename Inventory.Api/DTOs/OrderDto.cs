using System.ComponentModel.DataAnnotations;

namespace WareSync.Api.DTOs;
public class OrderDto
{
    [Key]
    public int OrderID { get; set; }
    public DateTime OrderDate { get; set; }
    public int ProviderID { get; set; }
    public string? ProviderName { get; set; }
    public List<OrderDetailDto> OrderDetails { get; set; } = new();
    public string Status { get; set; } // Added for order status
    public int WarehouseId { get; set; } // Added for warehouse reference
    public string? RejectReason { get; set; } // Added for rejected orders
}