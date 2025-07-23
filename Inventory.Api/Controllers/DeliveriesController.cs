using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.Mvc;
using WareSync.Business;
using WareSync.Api.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.OData.Formatter;
using WareSync.Api;
using WareSync.Domain;
using Microsoft.AspNetCore.Authorization;

namespace WareSync.Api;
[Route("api/[controller]")]
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
    //[Authorize(Roles = $"{CustomRoles.DeliveryStaff},{CustomRoles.WarehouseManager}")]
    public IQueryable<Delivery> Get()
    {
        return _deliveryBusiness.GetAllDeliveriesAsync();
    }

    [EnableQuery]
    [HttpGet("{key}")]
    //[Authorize(Roles = $"{CustomRoles.DeliveryStaff},{CustomRoles.WarehouseManager}")]
    public async Task<IActionResult> Get([FromODataUri] int key)
    {
        var delivery = await _deliveryBusiness.GetDeliveryByIdAsync(key);
        if (delivery == null) return NotFound();
        var dto = _mapper.Map<DeliveryDto>(delivery);
        return Ok(dto);
    }

    [HttpPost]
    //[Authorize(Roles = CustomRoles.WarehouseManager)]
    public async Task<IActionResult> Post([FromBody] CreateDeliveryDto dto)
    {
        var created = await _deliveryBusiness.CreateDeliveryAsync(dto);
        return Created(created);
    }

    [HttpPut("{key}")]
    //[Authorize(Roles = CustomRoles.WarehouseManager)]
    public async Task<IActionResult> Put([FromODataUri] int key, [FromBody] UpdateDeliveryDto dto)
    {
        var updated = await _deliveryBusiness.UpdateDeliveryAsync(dto, key);
        var result = _mapper.Map<DeliveryDto>(updated);
        return Updated(result);
    }

/*    [HttpDelete("{key}")]
    //[Authorize(Roles = CustomRoles.WarehouseManager)]
    public async Task<IActionResult> Delete([FromODataUri] int key)
    {
        await _deliveryBusiness.DeleteDeliveryAsync(key);
        return NoContent();
    }*/
} 