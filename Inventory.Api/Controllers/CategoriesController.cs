using Inventory.Domain;
using Inventory.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace Inventory.Api
{
    //[Authorize(CustomRoles.WarehouseStaff)]

    public class CategoriesController : ODataController
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService) => _categoryService = categoryService;

        [EnableQuery]
        public async Task<IActionResult> Get()
        {
            var result = await _categoryService.GetAllAsync();
            return Ok(result);
        }

        [EnableQuery]
        public async Task<IActionResult> Get([FromRoute] Guid key)
        {
            var result = await _categoryService.GetByIdAsync(key);
            return result is not null ? Ok(result) : NotFound();
        }

        public async Task<IActionResult> Post([FromBody] CreateCategoryDto dto)
        {
            var created = await _categoryService.CreateAsync(dto);
            return Created(created);
        }
        public async Task<IActionResult> Put([FromRoute] Guid key, [FromBody] UpdateCategoryDto dto)
        {
            if (key != dto.Id) return BadRequest();

            var updated = await _categoryService.UpdateAsync(dto);
            return updated is not null ? Ok(updated) : NotFound();
        }
        public async Task<IActionResult> Delete([FromRoute] Guid key)
        {
            var deleted = await _categoryService.DeleteAsync(key);
            return deleted ? NoContent() : NotFound();
        }
    }
}
