using WareSync.Domain;

namespace WareSync.Business;
public interface IReportBusiness
{
    // Tuỳ ý mở rộng các hàm báo cáo
    Task<object> GetInventoryReportAsync();
    Task<object> GetOrderReportAsync();
    Task<object> GetDeliveryReportAsync();
} 