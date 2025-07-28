using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WareSync.Repositories;
using System.Linq;

namespace WareSync.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly DataContext _dataContext;
        public ReportsController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        // GET: api/reports/transactions?from=2024-07-01&to=2024-07-10
        [HttpGet("transactions")]
        public IActionResult GetTransactions([FromQuery] DateTime? from, [FromQuery] DateTime? to)
        {
            var fromDate = from ?? DateTime.MinValue;
            var toDate = to ?? DateTime.MaxValue;

            // IN: Orders (import)
            var orderDetails = _dataContext.OrderDetails
                .Include(od => od.Order)
                .Include(od => od.Product)
                .Include(od => od.Order.Warehouse)
                .Include(od => od.Order.Provider)
                .Where(od => od.Order.OrderDate >= fromDate && od.Order.OrderDate <= toDate)
                .Select(od => new
                {
                    Date = od.Order.OrderDate,
                    Product = od.Product.ProductName,
                    ProductCode = od.Product.ProductCode,
                    Quantity = od.OrderQuantity,
                    Type = "in",
                    Color = "green",
                    Warehouse = od.Order.Warehouse.WarehouseName,
                    Partner = od.Order.Provider.ProviderName
                });

            // OUT: Deliveries (export)
            var deliveryDetails = _dataContext.DeliveryDetails
                .Include(dd => dd.Delivery)
                .Include(dd => dd.Product)
                .Include(dd => dd.Delivery.Customer)
                .Where(dd => dd.Delivery.SalesDate >= fromDate && dd.Delivery.SalesDate <= toDate)
                .Select(dd => new
                {
                    Date = dd.Delivery.SalesDate,
                    Product = dd.Product.ProductName,
                    ProductCode = dd.Product.ProductCode,
                    Quantity = dd.DeliveryQuantity,
                    Type = "out",
                    Color = "red",
                    Warehouse = "", // If you want to add warehouse, adjust model
                    Partner = dd.Delivery.Customer.CustomerName
                });

            var all = orderDetails.ToList().Concat(deliveryDetails.ToList())
                .OrderByDescending(x => x.Date)
                .ToArray();

            return Ok(all);
        }
    }
} 