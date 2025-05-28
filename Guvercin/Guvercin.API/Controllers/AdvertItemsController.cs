using Guvercin.Application.Dtos.AdvertItemsDtos;
using Guvercin.Application.Dtos.ResponseDtos;
using Guvercin.Application.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Guvercin.API.Controllers;

[Route("api/[controller]")]
[ApiController]

public class AdvertItemsController : BaseController
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
        return CreateResponse(result);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAdvertItemById(int id)
    {
        var result= await _advertItemServices.GetByIdAdvertItem(id);
        return CreateResponse(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> AddAdvertItem(CreateAdvertItemDto createAdvertItemDto)
    {
        var result =await _advertItemServices.AddAdvertItem(createAdvertItemDto);
        return CreateResponse(result);
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateAdvertItem(UpdateAdvertItemDto updateAdvertItemDto)
    {
        var result =await _advertItemServices.UpdateAdvertItem(updateAdvertItemDto);
        return CreateResponse(result);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAdvertItem(int id)
    {
        var result =await _advertItemServices.DeleteAdvertItem(id);
        return CreateResponse(result);
    }
}