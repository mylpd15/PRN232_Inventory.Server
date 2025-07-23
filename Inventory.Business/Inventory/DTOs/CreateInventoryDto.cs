using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WareSync.Business
{
    public class CreateInventoryDto
    {
        public int QuantityAvailable { get; set; }
        public int MinimumStockLevel { get; set; }
        public int MaximumStockLevel { get; set; }
        public int ReorderPoint { get; set; }
        public int ProductID { get; set; }
        public int WarehouseID { get; set; }
    }
}
