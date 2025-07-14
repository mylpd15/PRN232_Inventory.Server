using System.ComponentModel.DataAnnotations;
namespace Inventory.Domain
{
    public class InventoryEntry
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public InventoryEntryType EntryType { get; set; }

        [Required]
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        // History Log
        public Guid? UserId { get; set; }
        public AppUser? User { get; set; }
    }
}
