using Guvercin.Application.Dtos.AuthDtos;
using Guvercin.Application.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Guvercin.API.Controllers;

[Route("api/[controller]")]
[ApiController]

public class AuthController: BaseController
{
    private readonly IAuthServices _authServices;

    public AuthController(IAuthServices authServices)
    {
        _authServices = authServices;
    }

    [HttpPost("generateToken")]
    public async Task<IActionResult> GenerateToken(TokenDto dto)
    {
        var result = await _authServices.GenarateToken(dto);
        return CreateResponse(result);
    }
}