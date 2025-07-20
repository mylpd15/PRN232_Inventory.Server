using System.Text.Json.Serialization;

namespace WareSync.Business;
public class UpdateDeliveryDto
{
    public DateTime SalesDate { get; set; }
    public int CustomerID { get; set; }

}