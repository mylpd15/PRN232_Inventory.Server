using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using WareSync.Api.DTOs;
using WareSync.Business;
using WareSync.Domain;

namespace WareSync.Api;
[Route("odata/[controller]")]
public class InventoriesController : ODataController
{
    private readonly IInventoryBusiness _inventoryBusiness;
    private readonly IMapper _mapper;
    public InventoriesController(IInventoryBusiness inventoryBusiness, IMapper mapper)
    {
        _inventoryBusiness = inventoryBusiness;
        _mapper = mapper;
    }
    [EnableQuery]
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var inventories = await _inventoryBusiness.GetAllInventoriesAsync();
        var dtos = _mapper.Map<IEnumerable<InventoryDto>>(inventories);
        return Ok(dtos);
    }
    [EnableQuery]
    [HttpGet("{key}")]
    public async Task<IActionResult> Get([FromODataUri] int key)
    {
        var inventory = await _inventoryBusiness.GetInventoryByIdAsync(key);
        if (inventory == null) return NotFound();
        var dto = _mapper.Map<InventoryDto>(inventory);
        return Ok(dto);
    }
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateInventoryDto inventoryDto)
    {
        var inventory = _mapper.Map<Inventory>(inventoryDto);
        var created = await _inventoryBusiness.CreateInventoryAsync(inventory);
        var resultDto = _mapper.Map<InventoryDto>(created);
        return Created(resultDto);
    }
    [HttpPut("{key}")]
    public async Task<IActionResult> Put([FromODataUri] int key, [FromBody] UpdateInventoryDto inventoryDto)
    {
        if (key != inventoryDto.InventoryID)
            return BadRequest("Inventory ID mismatch");

        var inventory = _mapper.Map<Inventory>(inventoryDto);
        var updated = await _inventoryBusiness.UpdateInventoryAsync(inventory);
        var resultDto = _mapper.Map<InventoryDto>(updated);
        return Updated(resultDto);
    }
    [HttpDelete("{key}")]
    public async Task<IActionResult> Delete([FromODataUri] int key)
    {
        await _inventoryBusiness.DeleteInventoryAsync(key);
        return NoContent();
    }

}