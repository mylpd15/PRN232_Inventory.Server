using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using WareSync.Business;
using WareSync.Domain;

namespace WareSync.Api;

[Route("api/[controller]")]
public class InventoryLogsController : ODataController
{
    private readonly IInventoryLogBusiness _inventoryLogBusiness;
    private readonly IMapper _mapper;

    public InventoryLogsController(IInventoryLogBusiness inventoryLogBusiness, IMapper mapper)
    {
        _inventoryLogBusiness = inventoryLogBusiness;
        _mapper = mapper;
    }

    [EnableQuery]
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var logs = await _inventoryLogBusiness.GetInventoryLogsAsync();
        var dtos = _mapper.Map<IEnumerable<InventoryLogDto>>(logs);
        return Ok(dtos);
    }

    [EnableQuery]
    [HttpGet("{key}")]
    public async Task<IActionResult> Get([FromODataUri] int key)
    {
        var log = await _inventoryLogBusiness.GetInventoryLogByIdAsync(key);
        if (log == null) return NotFound();
        var dto = _mapper.Map<InventoryLogDto>(log);
        return Ok(dto);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateInventoryLogDto dto)
    {
        var log = _mapper.Map<InventoryLog>(dto);
        var created = await _inventoryLogBusiness.CreateInventoryLogAsync(log);
        var result = _mapper.Map<InventoryLogDto>(created);
        return Created(result);
    }
}
