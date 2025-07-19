using System.ComponentModel.DataAnnotations;

namespace WareSync.Domain;
public class Customer : AuditableEntity
{
    [Key]
    public int CustomerID { get; set; }
    [Required, MaxLength(100)]
    public string CustomerName { get; set; } = string.Empty;
    [MaxLength(200)]
    public string? CustomerAddress { get; set; }
    public ICollection<Delivery>? Deliveries { get; set; }
} 