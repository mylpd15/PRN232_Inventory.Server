namespace WareSync.Business;

public class UpdateOrderStatusDto
{
    public string Status { get; set; }
    public string? RejectReason { get; set; } // Optional for rejected orders
} 