using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Inventory.Domain;

namespace Inventory.Api;
[Authorize(CustomRoles.Admin)]
[Route("api/[controller]")]
[ApiController]
public class AdminController : ControllerBase
{
}
