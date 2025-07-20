using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WareSync.Business.Customer.DTOs
{
    public class CreateDeliveryDetailDto
    {
        public int ProductID { get; set; }
        public int DeliveryQuantity { get; set; }
        public DateTime ExpectedDate { get; set; }
    }

}
