using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using WareSync.Api.DTOs;
using WareSync.Business;
using WareSync.Domain;

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
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ProductDto productDto)
    {
        var product = _mapper.Map<Product>(productDto);
        var created = await _productBusiness.CreateProductAsync(product);
        return Created(created);
    }
    [HttpPut("{key}")]
    public async Task<IActionResult> Put([FromODataUri] int key, [FromBody] ProductDto productDto)
    {
        if (key != productDto.ProductID) return BadRequest("ID mismatch");
        var product = _mapper.Map<Product>(productDto);
        var updated = await _productBusiness.UpdateProductAsync(product);
        return Updated(updated);
    }
    [HttpDelete("{key}")]
    public async Task<IActionResult> Delete([FromODataUri] int key)
    {
        await _productBusiness.DeleteProductAsync(key);
        return NoContent();
    }

}