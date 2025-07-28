using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.Mvc;
using WareSync.Business;
using WareSync.Api.DTOs;
using WareSync.Domain;
using AutoMapper;
using Microsoft.AspNetCore.OData.Formatter;

namespace WareSync.Api;

[Route("api/[controller]")]
public class OrderDetailsController : ODataController
{
    private readonly IOrderDetailBusiness _orderDetailBusiness;
    private readonly IMapper _mapper;
    public OrderDetailsController(IOrderDetailBusiness orderDetailBusiness, IMapper mapper)
    {
        _orderDetailBusiness = orderDetailBusiness;
        _mapper = mapper;
    }
    [EnableQuery]
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var entities = await _orderDetailBusiness.GetAllOrderDetailsAsync();
        var dtos = _mapper.Map<IEnumerable<OrderDetailDto>>(entities);
        return Ok(dtos);
    }

    [EnableQuery]
    [HttpGet("{key}")]
    public async Task<IActionResult> Get([FromODataUri] int key)
    {
        var entity = await _orderDetailBusiness.GetOrderDetailByIdAsync(key);
        if (entity == null) return NotFound();
        var dto = _mapper.Map<OrderDetailDto>(entity);
        return Ok(dto);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] WareSync.Business.CreateOrderDetailDto dto)
    {
        var entity = _mapper.Map<OrderDetail>(dto);
        var created = await _orderDetailBusiness.CreateOrderDetailAsync(entity);
        var result = _mapper.Map<OrderDetailDto>(created);
        return CreatedAtAction(nameof(Get), new { key = result.OrderDetailID }, result);
    }

    [HttpPut("{key}")]
    public async Task<IActionResult> Put([FromODataUri] int key, [FromBody] UpdateOrderDetailDto dto)
    {
        if (key != dto.OrderDetailID)
            return BadRequest("Key mismatch");
        
        var entity = _mapper.Map<OrderDetail>(dto);
        var updated = await _orderDetailBusiness.UpdateOrderDetailAsync(entity);
        var result = _mapper.Map<OrderDetailDto>(updated);
        return Ok(result);
    }

    [HttpDelete("{key}")]
    public async Task<IActionResult> Delete([FromODataUri] int key)
    {
        await _orderDetailBusiness.DeleteOrderDetailAsync(key);
        return NoContent();
    }
} 