using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using WareSync.Api.DTOs;
using WareSync.Business;
using WareSync.Business.Provider.ProductWithPrice.DTOs;
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
    public async Task<IActionResult> Post([FromBody] CreateProductWithPriceDto productWithPriceDto)
    {
        var product = _mapper.Map<Product>(productWithPriceDto.Product);
        var price = _mapper.Map<ProductPrice>(productWithPriceDto.ProductPrice);

        var created = await _productBusiness.CreateProductWithPriceAsync(product, price);
        var resultDto = _mapper.Map<ProductDto>(created);   
        return Created(resultDto);
    }

    [HttpPut("{key}")]
    public async Task<IActionResult> Put([FromODataUri] int key,[FromBody] UpdateProductWithPriceDto dto)
   {
        if (key != dto.Product.ProductID)
            return BadRequest("ID mismatch");

        // Map DTO → Domain
        var product = _mapper.Map<Product>(dto.Product);
        var price = _mapper.Map<ProductPrice>(dto.ProductPrice);

        var updated = await _productBusiness.UpdateProductWithPriceAsync(product, price);

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