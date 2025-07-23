using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WareSync.Domain
{
    public class InventoryLog : AuditableEntity
    {
        [Key]
        public int LogID { get; set; }

        public int InventoryID { get; set; }
        [ForeignKey("InventoryID")]
        public Inventory? Inventory { get; set; }
        public string ActionType { get; set; } = string.Empty; // e.g. "Create", "Update", "Delete", "Import", "Export"
        public int ChangedQuantity { get; set; }
        public string? Description { get; set; }
    }
}
