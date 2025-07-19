using System.ComponentModel.DataAnnotations;

namespace WareSync.Domain;
public class Location : AuditableEntity
{
    [Key]
    public int LocationID { get; set; }
    [Required, MaxLength(100)]
    public string LocationName { get; set; } = string.Empty;
    [MaxLength(200)]
    public string? LocationAddress { get; set; }
    public ICollection<Warehouse>? Warehouses { get; set; }
} 