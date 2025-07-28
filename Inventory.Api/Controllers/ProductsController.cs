using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using WareSync.Api.DTOs;
using WareSync.Business;
using WareSync.Domain;

namespace WareSync.Api;
[Route("api/[controller]")]
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
    public async Task<IActionResult> Post([FromBody] CreateProductDto productDto)
    {
        if (productDto == null)
            return BadRequest("Product data is missing or invalid JSON.");

        var product = _mapper.Map<Product>(productDto);
        if (product == null)
            return BadRequest("Failed to map product data.");

        var created = await _productBusiness.CreateProductAsync(product);
        if (created == null)
            return StatusCode(500, "Product creation failed.");

        var resultDto = _mapper.Map<ProductDto>(created);
        return CreatedAtAction(nameof(Post), new { id = created.ProductID }, resultDto);
    }

    [HttpPut("{key}")]
    public async Task<IActionResult> Put([FromODataUri] int key, [FromBody] UpdateProductDto productDto)
    {
        if (key != productDto.ProductID) return BadRequest("ID mismatch");
        var product = _mapper.Map<Product>(productDto);
        var updated = await _productBusiness.UpdateProductAsync(product);
        var resultDto = _mapper.Map<ProductDto>(updated);
        return Updated(resultDto);
    }
    [HttpDelete("{key}")]
    public async Task<IActionResult> Delete([FromODataUri] int key)
    {
        await _productBusiness.DeleteProductAsync(key);
        return NoContent();
    }

}