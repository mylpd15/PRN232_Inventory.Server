<<<<<<< HEAD
﻿using Inventory.Domain;
using Inventory.Services.InventoryService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Routing.Controllers;
namespace Inventory.Api
{
    //[Authorize(CustomRoles.Coordinator)]

    public class InventoriesController : ODataController
    {
        private readonly IInventoryService _inventoryService;

        public InventoriesController(IInventoryService inventoryService) => _inventoryService = inventoryService;

        // POST: odata/Inventories
        public async Task<IActionResult> Post([FromBody] CreateInventoryEntryDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var created = await _inventoryService.CreateEntryAsync(dto);
            return Created(created);
        }

        // GET /odata/History(productId=..., page=1, take=10)
        [HttpGet("Inventory.History")]
        public async Task<IActionResult> History([FromODataUri] Guid productId, [FromODataUri] int page, [FromODataUri] int take)
        {
            var result = await _inventoryService.GetHistoryAsync(productId, page, take);
            return Ok(result);
        }
    }
}
=======
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
>>>>>>> develop
