using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WareSync.Business
{
    public class CreateInventoryLogDto
    {
        public int InventoryID { get; set; }
        public string ActionType { get; set; } = string.Empty; // e.g. "Create", "Update", "Import", "Export"
        public int ChangedQuantity { get; set; }
        public string? Description { get; set; }
    }
}
