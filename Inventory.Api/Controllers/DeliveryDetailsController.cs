using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.Mvc;
using WareSync.Business;
using WareSync.Api.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.OData.Formatter;
using WareSync.Domain;

namespace WareSync.Api;
[Route("odata/[controller]")]
public class DeliveryDetailsController : ODataController
{
    private readonly IDeliveryDetailBusiness _deliveryDetailBusiness;
    private readonly IMapper _mapper;
    public DeliveryDetailsController(IDeliveryDetailBusiness deliveryDetailBusiness, IMapper mapper)
    {
        _deliveryDetailBusiness = deliveryDetailBusiness;
        _mapper = mapper;
    }
    [EnableQuery]
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var entities = await _deliveryDetailBusiness.GetAllDeliveryDetailsAsync();
        var dtos = _mapper.Map<IEnumerable<DeliveryDetailDto>>(entities);
        return Ok(dtos);
    }
    [EnableQuery]
    [HttpGet("{key}")]
    public async Task<IActionResult> Get([FromODataUri] int key)
    {
        var entity = await _deliveryDetailBusiness.GetDeliveryDetailByIdAsync(key);
        if (entity == null) return NotFound();
        var dto = _mapper.Map<DeliveryDetailDto>(entity);
        return Ok(dto);
    }
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] DeliveryDetailDto dto)
    {
        var entity = _mapper.Map<DeliveryDetail>(dto);
        var created = await _deliveryDetailBusiness.CreateDeliveryDetailAsync(entity);
        return Created(created);
    }
    [HttpPut("{key}")]
    public async Task<IActionResult> Put([FromODataUri] int key, [FromBody] DeliveryDetailDto dto)
    {
        var entity = _mapper.Map<DeliveryDetail>(dto);
        entity.DeliveryDetailID = key;
        var updated = await _deliveryDetailBusiness.UpdateDeliveryDetailAsync(entity);
        return Updated(updated);
    }
    [HttpDelete("{key}")]
    public async Task<IActionResult> Delete([FromODataUri] int key)
    {
        await _deliveryDetailBusiness.DeleteDeliveryDetailAsync(key);
        return NoContent();
    }
} 