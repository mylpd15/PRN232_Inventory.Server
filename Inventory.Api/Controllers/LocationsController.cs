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
public class LocationsController : ODataController
{
    private readonly ILocationBusiness _locationBusiness;
    private readonly IMapper _mapper;
    public LocationsController(ILocationBusiness locationBusiness, IMapper mapper)
    {
        _locationBusiness = locationBusiness;
        _mapper = mapper;
    }
    [EnableQuery]
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var entities = await _locationBusiness.GetAllLocationsAsync();
        var dtos = _mapper.Map<IEnumerable<LocationDto>>(entities);
        return Ok(dtos);
    }
    [EnableQuery]
    [HttpGet("{key}")]
    public async Task<IActionResult> Get([FromODataUri] int key)
    {
        var entity = await _locationBusiness.GetLocationByIdAsync(key);
        if (entity == null) return NotFound();
        var dto = _mapper.Map<LocationDto>(entity);
        return Ok(dto);
    }
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] LocationDto dto)
    {
        var entity = _mapper.Map<Location>(dto);
        var created = await _locationBusiness.CreateLocationAsync(entity);
        return Created(created);
    }
    [HttpPut("{key}")]
    public async Task<IActionResult> Put([FromODataUri] int key, [FromBody] LocationDto dto)
    {
        var entity = _mapper.Map<Location>(dto);
        entity.LocationID = key;
        var updated = await _locationBusiness.UpdateLocationAsync(entity);
        return Updated(updated);
    }
    [HttpDelete("{key}")]
    public async Task<IActionResult> Delete([FromODataUri] int key)
    {
        await _locationBusiness.DeleteLocationAsync(key);
        return NoContent();
    }
} 