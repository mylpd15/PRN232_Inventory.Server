using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WareSync.Repositories;

namespace WareSync.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public DashboardController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        // GET: api/dashboard/summary
        [HttpGet("summary")]
        public IActionResult GetSummary()
        {
            // Tổng doanh thu (totalSales) và tổng số lượng bán (unitSold)
            var deliveries = _dataContext.Deliveries.Include(d => d.DeliveryDetails).Where(d => d.Status == Domain.Enums.DeliveryStatus.Delivered).ToList();
            var deliveryDetails = deliveries.SelectMany(d => d.DeliveryDetails ?? new List<Domain.DeliveryDetail>()).ToList();
            var productIds = deliveryDetails.Select(dd => dd.ProductID).Distinct().ToList();
            var prices = _dataContext.ProductPrices.Where(pp => pp.IsActive && productIds.Contains(pp.ProductID)).ToList();
            decimal totalSales = 0;
            int unitSold = 0;
            foreach (var dd in deliveryDetails)
            {
                var price = prices.FirstOrDefault(p => p.ProductID == dd.ProductID)?.SellingPrice ?? 0;
                totalSales += price * dd.DeliveryQuantity;
                unitSold += dd.DeliveryQuantity;
            }
            // Số lượng sản phẩm hết hàng (outOfStock)
            var outOfStock = _dataContext.Products
                .Where(p => p.Inventories.Sum(i => i.QuantityAvailable) == 0)
                .Count();
            // So sánh tuần này và tuần trước
            var now = DateTime.Now;
            var startOfThisWeek = now.Date.AddDays(-(int)now.DayOfWeek + 1); // Monday
            var startOfLastWeek = startOfThisWeek.AddDays(-7);
            var endOfLastWeek = startOfThisWeek.AddDays(-1);
            var thisWeekDetails = deliveryDetails.Where(dd => dd.Delivery?.SalesDate >= startOfThisWeek).ToList();
            var lastWeekDetails = deliveryDetails.Where(dd => dd.Delivery?.SalesDate >= startOfLastWeek && dd.Delivery?.SalesDate < startOfThisWeek).ToList();
            decimal salesThisWeek = thisWeekDetails.Sum(dd => (prices.FirstOrDefault(p => p.ProductID == dd.ProductID)?.SellingPrice ?? 0) * dd.DeliveryQuantity);
            decimal salesLastWeek = lastWeekDetails.Sum(dd => (prices.FirstOrDefault(p => p.ProductID == dd.ProductID)?.SellingPrice ?? 0) * dd.DeliveryQuantity);
            int unitsThisWeek = thisWeekDetails.Sum(dd => dd.DeliveryQuantity);
            int unitsLastWeek = lastWeekDetails.Sum(dd => dd.DeliveryQuantity);
            var summary = new
            {
                totalSales = totalSales,
                unitSold = unitSold,
                outOfStock = outOfStock,
                compareLastWeek = new
                {
                    sales = (int)(salesThisWeek - salesLastWeek),
                    units = unitsThisWeek - unitsLastWeek
                }
            };
            return Ok(summary);
        }


        // GET: api/dashboard/sales-daily
        [HttpGet("sales-daily")]
        public IActionResult GetSalesByDay()
        {
            var now = DateTime.Now;
            var startOfWeek = now.Date.AddDays(-6); // Monday
            var deliveries = _dataContext.Deliveries.Include(d => d.DeliveryDetails)
                .Where(d => d.Status == Domain.Enums.DeliveryStatus.Delivered && d.SalesDate >= startOfWeek)
                .ToList();
            var prices = _dataContext.ProductPrices.Where(pp => pp.IsActive).ToList();
            var sales = Enumerable.Range(0, 7).Select(i =>
            {
                var day = startOfWeek.AddDays(i);
                var dayDeliveries = deliveries.Where(d => d.SalesDate.Date == day.Date).ToList();
                var dayDetails = dayDeliveries.SelectMany(d => d.DeliveryDetails ?? new List<Domain.DeliveryDetail>()).ToList();
                var value = dayDetails.Sum(dd => (prices.FirstOrDefault(p => p.ProductID == dd.ProductID)?.SellingPrice ?? 0) * dd.DeliveryQuantity);
                return new { day = day.ToString("dd/MM/yyyy"), value = value };
            }).ToArray();
            return Ok(sales);
        }

        // GET: api/dashboard/out-of-stock?limit=3
        [HttpGet("out-of-stock")]
        public IActionResult GetOutOfStockProducts([FromQuery] int limit = 3)
        {
            var products = _dataContext.Products
            .Include(p => p.Inventories)
            .Include(p => p.Prices)
            .Where(p => p.Inventories.Sum(i => i.QuantityAvailable) == 0)
            .ToList() 
            .Select(p => new
            {
                name = p.ProductName,
                sku = p.ProductCode,
                price = p.Prices.FirstOrDefault(pp => pp.IsActive)?.SellingPrice ?? 0
            })
            .Take(limit)
            .ToArray();
            return Ok(products);
        }

        // GET: api/dashboard/top-selling?limit=3
        [HttpGet("top-selling")]
        public IActionResult GetTopSellingProducts([FromQuery] int limit = 3)
        {
            var deliveries = _dataContext.Deliveries.Include(d => d.DeliveryDetails).Where(d => d.Status == Domain.Enums.DeliveryStatus.Delivered).ToList();
            var deliveryDetails = deliveries.SelectMany(d => d.DeliveryDetails ?? new List<Domain.DeliveryDetail>()).ToList();
            var prices = _dataContext.ProductPrices.Where(pp => pp.IsActive).ToList();
            var topProducts = deliveryDetails
                .GroupBy(dd => dd.ProductID)
                .Select(g => new
                {
                    name = _dataContext.Products.FirstOrDefault(p => p.ProductID == g.Key)!.ProductName,
                    sold = g.Sum(dd => dd.DeliveryQuantity),
                    revenue = g.Sum(dd => (prices.FirstOrDefault(p => p.ProductID == g.Key)?.SellingPrice ?? 0) * dd.DeliveryQuantity)
                })
                .OrderByDescending(x => x.sold)
                .Take(limit)
                .ToArray();
            return Ok(topProducts);
        }
    }
}
