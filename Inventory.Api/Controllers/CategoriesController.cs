using Inventory.Domain;
using Inventory.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Api
{
    //[Authorize(CustomRoles.WarehouseStaff)]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService) => _categoryService = categoryService;

        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _categoryService.GetAllAsync());

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
            => (await _categoryService.GetByIdAsync(id)) is CategoryDto dto ? Ok(dto) : NotFound();

        [HttpPost]
        public async Task<IActionResult> Post(CreateCategoryDto dto)
        {
            var created = await _categoryService.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put(Guid id, UpdateCategoryDto dto)
        {
            if (id != dto.Id) return BadRequest();
            return (await _categoryService.UpdateAsync(dto)) is CategoryDto updated ? Ok(updated) : NotFound();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
            => await _categoryService.DeleteAsync(id) ? NoContent() : NotFound();
    }
}
