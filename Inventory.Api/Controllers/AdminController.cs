using WareSync.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace WareSync.Api;
[Authorize(CustomRoles.Admin)]
[Route("odata/[controller]")]
public class AdminController : ODataController
{
    // Thêm các action OData nếu cần
}
