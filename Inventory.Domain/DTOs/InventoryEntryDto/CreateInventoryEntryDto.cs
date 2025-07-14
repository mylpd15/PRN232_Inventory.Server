﻿using System.ComponentModel.DataAnnotations;
namespace Inventory.Domain
{
    public class CreateInventoryEntryDto
    {
        [Required]
        public Guid ProductId { get; set; }

        [Required, Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        [Required]
        public InventoryEntryType EntryType { get; set; }
    }
}
