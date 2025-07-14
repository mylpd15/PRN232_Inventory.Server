using Inventory.Domain;
using Inventory.Services.InventoryService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Inventory.Api
{
    //[Authorize(CustomRoles.Coordinator)]
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;

        public InventoryController(IInventoryService inventoryService) => _inventoryService = inventoryService;

        [HttpPost]
        public async Task<IActionResult> CreateEntry(CreateInventoryEntryDto dto)
            => Ok(await _inventoryService.CreateEntryAsync(dto));

        [HttpGet("history")]
        public async Task<IActionResult> History([FromQuery] Guid productId,
                                                 [FromQuery] int page = 1,
                                                 [FromQuery] int take = 10)
            => Ok(await _inventoryService.GetHistoryAsync(productId, page, take));
    }
}
