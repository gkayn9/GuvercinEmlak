using Guvercin.Application.Dtos.UserDtos;
using Guvercin.Application.Interfaces;
using Guvercin.Application.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Guvercin.API.Controllers;

[Route("api/[controller]")]
[ApiController]


public class UsersController : BaseController
{
    private readonly IUserServices _userServices;

    public UsersController(IUserServices userServices)
    {
        _userServices = userServices;
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register( RegisterDto registerDto)
    {
        var result = await _userServices.Register(registerDto);
        return CreateResponse(result);

    }
    [HttpPost("createRole")]
    public async Task<IActionResult> CreateRole(string roleName)
    {
        var result = await _userServices.CreateRole(roleName);
        return CreateResponse(result);
    }
    [HttpPost("addRoleToUser")]
    public async Task<IActionResult> AddRoleToUser(string email ,string roleName)
    {
        var result = await _userServices.AddToRole(email,roleName);
        return CreateResponse(result);
    }
}