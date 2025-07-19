using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.Mvc;
using WareSync.Business;
using WareSync.Api.DTOs;
using WareSync.Domain;
using AutoMapper;
using Microsoft.AspNetCore.OData.Formatter;

namespace WareSync.Api;
[Route("odata/[controller]")]
public class TransfersController : ODataController
{
    private readonly ITransferBusiness _transferBusiness;
    private readonly IMapper _mapper;
    public TransfersController(ITransferBusiness transferBusiness, IMapper mapper)
    {
        _transferBusiness = transferBusiness;
        _mapper = mapper;
    }
    [EnableQuery]
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var entities = await _transferBusiness.GetAllTransfersAsync();
        var dtos = _mapper.Map<IEnumerable<TransferDto>>(entities);
        return Ok(dtos);
    }
    [EnableQuery]
    [HttpGet("{key}")]
    public async Task<IActionResult> Get([FromODataUri] int key)
    {
        var entity = await _transferBusiness.GetTransferByIdAsync(key);
        if (entity == null) return NotFound();
        var dto = _mapper.Map<TransferDto>(entity);
        return Ok(dto);
    }
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] TransferDto dto)
    {
        var entity = _mapper.Map<Transfer>(dto);
        var created = await _transferBusiness.CreateTransferAsync(entity);
        return Created(created);
    }
    [HttpPut("{key}")]
    public async Task<IActionResult> Put([FromODataUri] int key, [FromBody] TransferDto dto)
    {
        var entity = _mapper.Map<Transfer>(dto);
        entity.TransferID = key;
        var updated = await _transferBusiness.UpdateTransferAsync(entity);
        return Updated(updated);
    }
    [HttpDelete("{key}")]
    public async Task<IActionResult> Delete([FromODataUri] int key)
    {
        await _transferBusiness.DeleteTransferAsync(key);
        return NoContent();
    }
} 