using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.Mvc;
using WareSync.Business;
using WareSync.Api.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.OData.Formatter;

namespace WareSync.Api;
[Route("odata/[controller]")]
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
    public async Task<IActionResult> Get()
    {
        var customers = await _customerBusiness.GetAllCustomersAsync();
        var dtos = _mapper.Map<IEnumerable<CustomerDto>>(customers);
        return Ok(dtos);
    }
    [EnableQuery]
    [HttpGet("{key}")]
    public async Task<IActionResult> Get([FromODataUri] int key)
    {
        var customer = await _customerBusiness.GetCustomerByIdAsync(key);
        if (customer == null) return NotFound();
        var dto = _mapper.Map<CustomerDto>(customer);
        return Ok(dto);
    }
} 