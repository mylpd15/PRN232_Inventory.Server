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
public class ProvidersController : ODataController
{
    private readonly IProviderBusiness _providerBusiness;
    private readonly IMapper _mapper;
    public ProvidersController(IProviderBusiness providerBusiness, IMapper mapper)
    {
        _providerBusiness = providerBusiness;
        _mapper = mapper;
    }
    [EnableQuery]
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var providers = await _providerBusiness.GetAllProvidersAsync();
        var dtos = _mapper.Map<IEnumerable<ProviderDto>>(providers);
        return Ok(dtos);
    }

    [EnableQuery]
    [HttpGet("{key}")]
    public async Task<IActionResult> Get([FromODataUri] int key)
    {
        var provider = await _providerBusiness.GetProviderByIdAsync(key);
        if (provider == null) return NotFound();
        var dto = _mapper.Map<ProviderDto>(provider);
        return Ok(dto);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateProviderDto createProviderDto)
    {
        var provider = _mapper.Map<Provider>(createProviderDto);
        var createdProvider = await _providerBusiness.CreateProviderAsync(provider);
        var dto = _mapper.Map<ProviderDto>(createdProvider);
        return CreatedAtAction(nameof(Get), new { key = dto.ProviderID }, dto);
    }

    [HttpPut("{key}")]
    public async Task<IActionResult> Put([FromODataUri] int key, [FromBody] UpdateProviderDto updateProviderDto)
    {
        if (key != updateProviderDto.ProviderID)
            return BadRequest("Key mismatch");

        var provider = _mapper.Map<Provider>(updateProviderDto);
        var updatedProvider = await _providerBusiness.UpdateProviderAsync(provider);
        var dto = _mapper.Map<ProviderDto>(updatedProvider);
        return Ok(dto);
    }

    [HttpDelete("{key}")]
    public async Task<IActionResult> Delete([FromODataUri] int key)
    {
        await _providerBusiness.DeleteProviderAsync(key);
        return NoContent();
    }
} 