using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WareSync.Domain.Enums
{
    public enum OrderStatus
    {
        Pending = 0,    // Đơn hàng mới tạo
        Approved = 1,   // Đơn hàng đã được duyệt
        Rejected = 2,   // Đơn hàng bị từ chối
        Completed = 3   // Đơn hàng đã hoàn tất
    }
}
