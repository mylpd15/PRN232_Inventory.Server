using Inventory.Domain;
using Inventory.Services.InventoryService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Routing.Controllers;
namespace Inventory.Api
{
    //[Authorize(CustomRoles.Coordinator)]

    public class InventoriesController : ODataController
    {
        private readonly IInventoryService _inventoryService;

        public InventoriesController(IInventoryService inventoryService) => _inventoryService = inventoryService;

        // POST: odata/Inventories
        public async Task<IActionResult> Post([FromBody] CreateInventoryEntryDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var created = await _inventoryService.CreateEntryAsync(dto);
            return Created(created);
        }

        // GET /odata/History(productId=..., page=1, take=10)
        [HttpGet("Inventory.History")]
        public async Task<IActionResult> History([FromODataUri] Guid productId, [FromODataUri] int page, [FromODataUri] int take)
        {
            var result = await _inventoryService.GetHistoryAsync(productId, page, take);
            return Ok(result);
        }
    }
}
