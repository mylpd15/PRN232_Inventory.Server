using System.ComponentModel.DataAnnotations;

namespace WareSync.Api
{
    public class InventoryLogDto
    {
        [Key]
        public int LogID { get; set; }
        public int InventoryID { get; set; }
        public string ActionType { get; set; } = string.Empty;
        public int ChangedQuantity { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
