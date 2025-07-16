using Inventory.Domain;
using Inventory.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace Inventory.Api
{
    //[Authorize(CustomRoles.WarehouseStaff)]
    public class ProductsController : ODataController
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
            => _productService = productService;

        // GET: odata/Products
        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(_productService.GetQueryable());
        }
        // GET: odata/Products(key)
        [EnableQuery]
        public async Task<IActionResult> Get([FromODataUri] Guid key)
        {
            var result = await _productService.GetByIdAsync(key);
            return result is not null ? Ok(result) : NotFound();
        }
        // POST: odata/Products
        public async Task<IActionResult> Post([FromBody] CreateProductDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var created = await _productService.CreateAsync(dto);
            return Created(created);
        }
        // PUT: odata/Products(key)
        public async Task<IActionResult> Put([FromODataUri] Guid key, [FromBody] UpdateProductDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (key != dto.Id) return BadRequest("ID mismatch.");

            var updated = await _productService.UpdateAsync(dto);
            return updated is not null ? Ok(updated) : NotFound();
        }
        // DELETE: odata/Products(key)
        public async Task<IActionResult> Delete([FromODataUri] Guid key)
        {
            var deleted = await _productService.DeleteAsync(key);
            return deleted ? NoContent() : NotFound();
        }
    }
}
