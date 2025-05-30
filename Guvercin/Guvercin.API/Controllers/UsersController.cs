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
}