using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WareSync.Domain.Enums
{
    public enum DeliveryStatus
    {
        Pending = 0,
        Shipped = 1,
        Delivered = 2,
        Cancelled = 3,
        Accepted = 4,
    }
}
