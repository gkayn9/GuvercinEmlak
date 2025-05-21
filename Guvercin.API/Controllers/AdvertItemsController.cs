using Guvercin.Application.Dtos.AdvertItemsDtos;
using Guvercin.Application.Dtos.ResponseDtos;
using Guvercin.Application.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Guvercin.API.Controllers;

[Route("api/[controller]")]
[ApiController]

public class AdvertItemsController : ControllerBase
{
    private readonly IAdvertItemServices _advertItemServices;

    public AdvertItemsController(IAdvertItemServices advertItemServices)
    {
        _advertItemServices = advertItemServices;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAdvertItems()
    {
        var result= await _advertItemServices.GetAllAdvertItems();
        if (!result.Success)
        {
            if (result.ErrorCodes==ErrorCodes.NotFound)
            {
                return Ok(result);
            } 
            return BadRequest(result);
        }
        return Ok(result);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAdvertItemById(int id)
    {
        var result= await _advertItemServices.GetByIdAdvertItem(id);
        if (!result.Success)
        {
            if (result.ErrorCodes==ErrorCodes.NotFound)
            {
                return Ok(result);
            } 
            return BadRequest(result);
        }
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> AddAdvertItem(CreateAdvertItemDto createAdvertItemDto)
    {
        var result =await _advertItemServices.AddAdvertItem(createAdvertItemDto);
        if (!result.Success)
        {
            if (result.ErrorCodes is ErrorCodes.ValidationError )
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        return Ok(result);
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateAdvertItem(UpdateAdvertItemDto updateAdvertItemDto)
    {
        var result =await _advertItemServices.UpdateAdvertItem(updateAdvertItemDto);
        if (!result.Success)
        {
            if (result.ErrorCodes is ErrorCodes.ValidationError or ErrorCodes.NotFound )
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        return Ok("İlan Güncellendi");
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAdvertItem(int id)
    {
        var result =await _advertItemServices.DeleteAdvertItem(id);
        if (!result.Success)
        {
            if (result.ErrorCodes == ErrorCodes.NotFound)
            {
                return Ok(result);
            }
        }
        return Ok("İlan Silindi");
    }
}