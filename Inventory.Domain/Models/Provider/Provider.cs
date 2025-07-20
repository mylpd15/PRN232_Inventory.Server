using System.ComponentModel.DataAnnotations;

namespace WareSync.Domain;
public class Provider : AuditableEntity
{
    [Key]
    public int ProviderID { get; set; }
    [Required, MaxLength(100)]
    public string ProviderName { get; set; } = string.Empty;
    [MaxLength(200)]
    public string? ProviderAddress { get; set; }
    public ICollection<Order>? Orders { get; set; }
} 