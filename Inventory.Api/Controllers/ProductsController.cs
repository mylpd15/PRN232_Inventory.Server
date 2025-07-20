using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.Mvc;
using WareSync.Business;
using WareSync.Api.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.OData.Formatter;

namespace WareSync.Api;
[Route("odata/[controller]")]
public class ProductsController : ODataController
{
    private readonly IProductBusiness _productBusiness;
    private readonly IMapper _mapper;
    public ProductsController(IProductBusiness productBusiness, IMapper mapper)
    {
        _productBusiness = productBusiness;
        _mapper = mapper;
    }
    [EnableQuery]
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var products = await _productBusiness.GetAllProductsAsync();
        var dtos = _mapper.Map<IEnumerable<ProductDto>>(products);
        return Ok(dtos);
    }
    [EnableQuery]
    [HttpGet("{key}")]
    public async Task<IActionResult> Get([FromODataUri] int key)
    {
        var product = await _productBusiness.GetProductByIdAsync(key);
        if (product == null) return NotFound();
        var dto = _mapper.Map<ProductDto>(product);
        return Ok(dto);
    }
} 