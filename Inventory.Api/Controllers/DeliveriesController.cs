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
public class DeliveriesController : ODataController
{
    private readonly IDeliveryBusiness _deliveryBusiness;
    private readonly IMapper _mapper;
    public DeliveriesController(IDeliveryBusiness deliveryBusiness, IMapper mapper)
    {
        _deliveryBusiness = deliveryBusiness;
        _mapper = mapper;
    }
    [EnableQuery]
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var deliveries = await _deliveryBusiness.GetAllDeliveriesAsync();
        var dtos = _mapper.Map<IEnumerable<DeliveryDto>>(deliveries);
        return Ok(dtos);
    }
    [EnableQuery]
    [HttpGet("{key}")]
    public async Task<IActionResult> Get([FromODataUri] int key)
    {
        var delivery = await _deliveryBusiness.GetDeliveryByIdAsync(key);
        if (delivery == null) return NotFound();
        var dto = _mapper.Map<DeliveryDto>(delivery);
        return Ok(dto);
    }
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateDeliveryDto dto)
    {
        var entity = _mapper.Map<Delivery>(dto);
        var created = await _deliveryBusiness.CreateDeliveryAsync(entity);
        var result = _mapper.Map<DeliveryDto>(created);
        return Created(result);
    }
    [HttpPut("{key}")]
    public async Task<IActionResult> Put([FromODataUri] int key, [FromBody] CreateDeliveryDto dto)
    {
        var entity = _mapper.Map<Delivery>(dto);
        entity.DeliveryID = key;
        var updated = await _deliveryBusiness.UpdateDeliveryAsync(entity);
        var result = _mapper.Map<DeliveryDto>(updated);
        return Updated(result);
    }
    [HttpDelete("{key}")]
    public async Task<IActionResult> Delete([FromODataUri] int key)
    {
        await _deliveryBusiness.DeleteDeliveryAsync(key);
        return NoContent();
    }
} 