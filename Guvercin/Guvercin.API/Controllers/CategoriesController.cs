using Guvercin.Application.Dtos.CategoryDtos;
using Guvercin.Application.Dtos.ResponseDtos;
using Guvercin.Application.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Guvercin.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : BaseController
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
        return CreateResponse(result);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategoryById(int id)
    {
        var result= await _categoryServices.GetCategoryById(id);
        return CreateResponse(result);
    }
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> AddCategory(CreateCategoryDto createCategoryDto)
    {
        var result=await _categoryServices.AddCategory(createCategoryDto);
        return CreateResponse(result);
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateCategory(UpdateCategoryDto updateCategoryDto)
    {
        
        var result =await _categoryServices.UpdateCategory(updateCategoryDto);
        return CreateResponse(result);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        var result=await _categoryServices.DeleteCategory(id);
        return CreateResponse(result);
    }
    
    [HttpGet("getCategoriesWithAdvertItem")]
    public async Task<IActionResult> GetAllCategoriesWithAdvertItem( )
    {
        var result = await _categoryServices.GetCategoriesWithAdvertItem();
        return CreateResponse(result);
    }
}