using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.Mvc;
using WareSync.Business;
using WareSync.Api.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.OData.Formatter;
using WareSync.Api;
using WareSync.Domain;
using WareSync.Domain.Enums;


namespace WareSync.Api;

[Route("api/[controller]")]
public class OrdersController : ODataController
{
    private readonly IOrderBusiness _orderBusiness;
    private readonly IMapper _mapper;
    public OrdersController(IOrderBusiness orderBusiness, IMapper mapper)
    {
        _orderBusiness = orderBusiness;
        _mapper = mapper;
    }
    [EnableQuery]
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var orders = await _orderBusiness.GetAllOrdersAsync();
        var dtos = _mapper.Map<IEnumerable<OrderDto>>(orders);
        return Ok(dtos);
    }

    [EnableQuery]
    [HttpGet("{key}")]
    public async Task<IActionResult> Get([FromODataUri] int key)
    {
        var order = await _orderBusiness.GetOrderByIdAsync(key);
        if (order == null) return NotFound();
        var dto = _mapper.Map<OrderDto>(order);
        return Ok(dto);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] WareSync.Business.CreateOrderDto dto)
    {
        var created = await _orderBusiness.CreateOrderAsync(dto);
        var result = _mapper.Map<OrderDto>(created);
        return CreatedAtAction(nameof(Get), new { key = result.OrderID }, result);
    }

    [HttpPut("{key}")]
    public async Task<IActionResult> Put([FromODataUri] int key, [FromBody] UpdateOrderDto dto)
    {
        if (key != dto.OrderID)
            return BadRequest("Key mismatch");

        // Convert UpdateOrderDto to CreateOrderDto for the business layer
        var createDto = new WareSync.Business.CreateOrderDto
        {
            OrderDate = dto.OrderDate,
            ProviderID = dto.ProviderID,
            WarehouseId = dto.WarehouseId, // Ensure WarehouseId is updated
            OrderDetails = dto.OrderDetails.Select(od => new WareSync.Business.CreateOrderDetailDto
            {
                ProductID = od.ProductID,
                OrderQuantity = od.OrderQuantity,
                ExpectedDate = od.ExpectedDate
            }).ToList()
        };

        var updated = await _orderBusiness.UpdateOrderAsync(key, createDto);
        var result = _mapper.Map<OrderDto>(updated);
        return Ok(result);
    }


    [HttpPatch("{orderId}/status")]
    public async Task<IActionResult> UpdateStatus([FromRoute] int orderId, [FromBody] UpdateOrderStatusDto dto)
    {
        if (!Enum.TryParse<OrderStatus>(dto.Status, true, out var newStatus))
            return BadRequest("Invalid status value");
        var updatedOrder = await _orderBusiness.UpdateOrderStatusAsync(orderId, newStatus, dto.RejectReason);
        var resultDto = _mapper.Map<OrderDto>(updatedOrder);
        return Ok(resultDto);
    }

    [HttpDelete("{key}")]
    public async Task<IActionResult> Delete([FromODataUri] int key)
    {
        await _orderBusiness.DeleteOrderAsync(key);
        return NoContent();
    }
} 