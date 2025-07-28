using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WareSync.Business;

public class CreateWarehouseDto
{
    public string WarehouseName { get; set; } = string.Empty;
    public bool IsRefrigerated { get; set; }
    public int LocationID { get; set; }
}
