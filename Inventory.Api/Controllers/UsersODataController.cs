using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using WareSync.Api.DTOs;
using WareSync.Business;
using WareSync.Domain;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace WareSync.Api;
[Route("odata/[controller]")]
[ApiExplorerSettings(IgnoreApi = true)]
public class UsersODataController : ODataController
{
    private readonly IUserBusiness _userBusiness;
    private readonly IMapper _mapper;

    public UsersODataController(IUserBusiness userBusiness, IMapper mapper)
    {
        _userBusiness = userBusiness;
        _mapper = mapper;
    }

    [EnableQuery]
    public async Task<ActionResult<IQueryable<UserDto>>> Get()
    {
        var users = await _userBusiness.GetAllUsersAsync();
        var dtos = _mapper.Map<IEnumerable<UserDto>>(users);
        return Ok(dtos.AsQueryable());
    }
}
