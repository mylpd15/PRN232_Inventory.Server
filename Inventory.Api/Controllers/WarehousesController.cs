using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.Mvc;
using WareSync.Business;
using WareSync.Api.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.OData.Formatter;
using WareSync.Domain;

namespace WareSync.Api;
[Route("api/[controller]")]
public class WarehousesController : ODataController
{
    private readonly IWarehouseBusiness _warehouseBusiness;
    private readonly IMapper _mapper;
    public WarehousesController(IWarehouseBusiness warehouseBusiness, IMapper mapper)
    {
        _warehouseBusiness = warehouseBusiness;
        _mapper = mapper;
    }
    [EnableQuery]
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var warehouses = await _warehouseBusiness.GetAllWarehousesAsync();
        var dtos = _mapper.Map<IEnumerable<WarehouseDto>>(warehouses);
        return Ok(dtos);
    }
    [EnableQuery]
    [HttpGet("{key}")]
    public async Task<IActionResult> Get([FromODataUri] int key)
    {
        var warehouse = await _warehouseBusiness.GetWarehouseByIdAsync(key);
        if (warehouse == null) return NotFound();
        var dto = _mapper.Map<WarehouseDto>(warehouse);
        return Ok(dto);
    }
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateWarehouseDto warehouseDto)
    {
        var warehouse = _mapper.Map<Warehouse>(warehouseDto);
        var created = await _warehouseBusiness.CreateWarehouseAsync(warehouse);
        return Created(created);
    }
    [HttpPut("{key}")]
    public async Task<IActionResult> Put([FromODataUri] int key, [FromBody] WarehouseDto dto)
    {
        var entity = _mapper.Map<Warehouse>(dto);
        entity.WarehouseID = key;
        var updated = await _warehouseBusiness.UpdateWarehouseAsync(entity);
        return Updated(updated);
    }
    [HttpDelete("{key}")]
    public async Task<IActionResult> Delete([FromODataUri] int key)
    {
        await _warehouseBusiness.DeleteWarehouseAsync(key);
        return NoContent();
    }
}