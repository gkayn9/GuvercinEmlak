using Guvercin.Application.Dtos.AdvertItemsDtos;
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
        return Ok(result);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAdvertItemById(int id)
    {
        var result= await _advertItemServices.GetByIdAdvertItem(id);
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> AddAdvertItem(CreateAdvertItemDto createAdvertItemDto)
    {
        await _advertItemServices.AddAdvertItem(createAdvertItemDto);
        return Ok("İlan Oluşturuldu");
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateAdvertItem(UpdateAdvertItemDto updateAdvertItemDto)
    {
        await _advertItemServices.UpdateAdvertItem(updateAdvertItemDto);
        return Ok("İlan Güncellendi");
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAdvertItem(int id)
    {
        await _advertItemServices.DeleteAdvertItem(id);
        return Ok("İlan Silindi");
    }
}