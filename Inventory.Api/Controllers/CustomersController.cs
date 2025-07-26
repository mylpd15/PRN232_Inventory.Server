using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.Mvc;
using WareSync.Business;
using WareSync.Api.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.Authorization;
using WareSync.Domain;

namespace WareSync.Api;
[Route("api/[controller]")]
public class CustomersController : ODataController
{
    private readonly ICustomerBusiness _customerBusiness;
    private readonly IMapper _mapper;
    public CustomersController(ICustomerBusiness customerBusiness, IMapper mapper)
    {
        _customerBusiness = customerBusiness;
        _mapper = mapper;
    }

    [EnableQuery]
    [HttpGet]
   // [Authorize(Roles = $"{CustomRoles.Admin},{CustomRoles.SalesStaff}")]
    public IQueryable<Customer> Get()
    {
        return _customerBusiness.GetAllCustomersAsync();
       
    }

    [EnableQuery]
    [HttpGet("{key}")]
    //[Authorize(Roles = $"{CustomRoles.Admin},{CustomRoles.SalesStaff}")]
    public async Task<IActionResult> Get([FromODataUri] int key)
    {
        var customer = await _customerBusiness.GetCustomerByIdAsync(key);
        if (customer == null) return NotFound();
        var dto = _mapper.Map<CustomerDto>(customer);
        return Ok(dto);
    }

    [HttpPost]
    //[Authorize(Roles = CustomRoles.Admin)]
    public async Task<IActionResult> Post([FromBody] CreateCustomerDto dto)
    {
        var entity = _mapper.Map<Customer>(dto);
        var created = await _customerBusiness.CreateCustomerAsync(entity);
        return Created(created);
    }

    [HttpPut("{key}")]
    //[Authorize(Roles = CustomRoles.Admin)]
    public async Task<IActionResult> Put([FromODataUri] int key, [FromBody] UpdateCustomerDto dto)
    {
        var entity = _mapper.Map<Customer>(dto);
        entity.CustomerID = key;
        var updated = await _customerBusiness.UpdateCustomerAsync(entity);
        return Updated(updated);
    }

    [HttpDelete("{key}")]
    //[Authorize(Roles = CustomRoles.Admin)]
    public async Task<IActionResult> Delete([FromODataUri] int key)
    {
        await _customerBusiness.DeleteCustomerAsync(key);
        return NoContent();
    }
} 