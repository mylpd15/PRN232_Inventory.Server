using Inventory.Domain;
using Inventory.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Api
{
    [Authorize(CustomRoles.WarehouseStaff)]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
            => _productService = productService;

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int page = 1,
                                             [FromQuery] int take = 10,
                                             [FromQuery] string? search = null)
            => Ok(await _productService.GetPagedAsync(page, take, search));

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
            => (await _productService.GetByIdAsync(id)) is ProductDto dto
               ? Ok(dto)
               : NotFound();

        [HttpPost]
        public async Task<IActionResult> Post(CreateProductDto dto)
        {
            var created = await _productService.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put(Guid id, UpdateProductDto dto)
        {
            if (id != dto.Id) return BadRequest();
            return (await _productService.UpdateAsync(dto)) is ProductDto updated
                   ? Ok(updated)
                   : NotFound();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
            => await _productService.DeleteAsync(id)
               ? NoContent()
               : NotFound();
    }
}
