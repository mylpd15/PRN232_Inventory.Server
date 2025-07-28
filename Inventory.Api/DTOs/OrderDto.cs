using System.ComponentModel.DataAnnotations;

namespace WareSync.Api.DTOs;
public class OrderDto
{
    [Key]
    public int OrderID { get; set; }
    public DateTime OrderDate { get; set; }

    public int ProviderID { get; set; }
    public ProviderDto? Provider { get; set; } // Navigation

    public int WarehouseId { get; set; }
    public WarehouseDto? Warehouse { get; set; } // Navigation

    public List<OrderDetailDto> OrderDetails { get; set; } = new();

    public string Status { get; set; }
    public string? RejectReason { get; set; }
}
