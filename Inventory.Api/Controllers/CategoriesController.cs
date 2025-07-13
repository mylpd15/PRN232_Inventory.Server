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
        private readonly ICategoryService _svc;

        public CategoriesController(ICategoryService svc) => _svc = svc;

        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _svc.GetAllAsync());

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
            => (await _svc.GetByIdAsync(id)) is CategoryDto dto ? Ok(dto) : NotFound();

        [HttpPost]
        public async Task<IActionResult> Post(CreateCategoryDto dto)
        {
            var created = await _svc.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put(Guid id, UpdateCategoryDto dto)
        {
            if (id != dto.Id) return BadRequest();
            return (await _svc.UpdateAsync(dto)) is CategoryDto updated ? Ok(updated) : NotFound();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
            => await _svc.DeleteAsync(id) ? NoContent() : NotFound();
    }
}
