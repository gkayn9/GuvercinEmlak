using Guvercin.Application.Dtos.CategoryDtos;
using Guvercin.Application.Dtos.ResponseDtos;
using Guvercin.Application.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Guvercin.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryServices _categoryServices;

    public CategoriesController(ICategoryServices categoryServices)
    {
        _categoryServices = categoryServices;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCatagories()
    {
        
        var result= await _categoryServices.GetAllCategories();
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
    public async Task<IActionResult> GetCategoryById(int id)
    {
        var result= await _categoryServices.GetCategoryById(id);
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
    public async Task<IActionResult> AddCategory(CreateCategoryDto createCategoryDto)
    {
        var result=await _categoryServices.AddCategory(createCategoryDto);
        if (!result.Success)
        {
            if (result.ErrorCodes==ErrorCodes.ValidationError)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        return Ok(result);
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateCategory(UpdateCategoryDto updateCategoryDto)
    {
        
        var result =await _categoryServices.UpdateCategory(updateCategoryDto);
        if (!result.Success)
        {
            if (result.ErrorCodes==ErrorCodes.NotFound || result.ErrorCodes==ErrorCodes.ValidationError)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        return Ok("Kategori Güncellendi");
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        var result=await _categoryServices.DeleteCategory(id);
        if (result.Success)
        {
            if (result.ErrorCodes==ErrorCodes.NotFound)
            {
                return Ok(result);
            }
            return BadRequest(result);
            
        }
        return Ok("Kategori Silindi");
    }
}