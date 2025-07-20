using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.Mvc;
using WareSync.Business;
using WareSync.Api.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.OData.Formatter;

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
} 