using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.Mvc;
using WareSync.Business;
using WareSync.Api.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.OData.Formatter;
using WareSync.Api;
using WareSync.Domain;


namespace WareSync.Api;
[Route("odata/[controller]")]
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
    public async Task<IActionResult> Post([FromBody] CreateOrderDto dto)
    {
        var entity = _mapper.Map<Order>(dto);
        var created = await _orderBusiness.CreateOrderAsync(entity);
        var result = _mapper.Map<OrderDto>(created);
        return Created(result);
    }
    [HttpPut("{key}")]
    public async Task<IActionResult> Put([FromODataUri] int key, [FromBody] CreateOrderDto dto)
    {
        var entity = _mapper.Map<Order>(dto);
        entity.OrderID = key;
        var updated = await _orderBusiness.UpdateOrderAsync(entity);
        var result = _mapper.Map<OrderDto>(updated);
        return Updated(result);
    }
    [HttpDelete("{key}")]
    public async Task<IActionResult> Delete([FromODataUri] int key)
    {
        await _orderBusiness.DeleteOrderAsync(key);
        return NoContent();
    }
} 