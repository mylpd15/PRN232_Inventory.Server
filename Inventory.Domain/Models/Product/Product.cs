using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Domain
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public Guid CategoryId { get; set; }

        // Navigation
        public Category Category { get; set; }

        public ICollection<InventoryEntry>? InventoryEntries { get; set; }
    }
}
