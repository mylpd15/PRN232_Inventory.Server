<<<<<<< HEAD
﻿using System;
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
=======
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WareSync.Domain;
public class Product : AuditableEntity
{
    [Key]
    public int ProductID { get; set; }
    [Required, MaxLength(100)]
    public string ProductCode { get; set; } = string.Empty;
    [MaxLength(100)]
    public string? BarCode { get; set; }
    [MaxLength(2000)]
    public string? ProductName { get; set; }
    [MaxLength(2000)]
    public string? ProductDescription { get; set; }
    [MaxLength(100)]
    public string? ProductCategory { get; set; }
    public int ReorderQuantity { get; set; }
    public decimal PackedWeight { get; set; }
    public decimal PackedHeight { get; set; }
    public decimal PackedWidth { get; set; }
    public decimal PackedDepth { get; set; }
    public bool Refrigerated { get; set; }
    // Navigation
    public ICollection<Inventory>? Inventories { get; set; }
    public ICollection<OrderDetail>? OrderDetails { get; set; }
    public ICollection<DeliveryDetail>? DeliveryDetails { get; set; }
} 
>>>>>>> develop
