using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.Mvc;
using WareSync.Business;
using WareSync.Api.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.OData.Formatter;

namespace WareSync.Api;
[Route("odata/[controller]")]
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
} 