namespace WareSync.Business;

public class UpdateOrderDto
{
    public int OrderID { get; set; }
    public DateTime OrderDate { get; set; }
    public int ProviderID { get; set; }
    public int WarehouseId { get; set; } // Added for warehouse reference
    public string? Status { get; set; } // Optional for status update
    public string? RejectReason { get; set; } // Optional for rejected orders
    public List<UpdateOrderDetailDto> OrderDetails { get; set; } = new();
}

public class UpdateOrderDetailDto
{
    public int OrderDetailID { get; set; }
    public int ProductID { get; set; }
    public int OrderQuantity { get; set; }
    public DateTime ExpectedDate { get; set; }
    public DateTime? ActualDate { get; set; }
} 