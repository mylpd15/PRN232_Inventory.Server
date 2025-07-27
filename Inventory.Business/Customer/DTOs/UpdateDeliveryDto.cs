using System.Text.Json.Serialization;
using WareSync.Domain.Enums;

namespace WareSync.Business;
public class UpdateDeliveryDto
{
    public DateTime SalesDate { get; set; }
    public int CustomerID { get; set; }
    public DeliveryStatus Status { get; set; }

}