using Guvercin.Application.Dtos.CategoryDtos;
using Guvercin.Application.Dtos.ResponseDtos;

namespace Guvercin.Application.Services.Abstract;

public interface ICategoryServices
{
    Task<ResponseDto<List<ResultCategoryDto>>>GetAllCategories();
    Task<ResponseDto<DetailCategoryDto>> GetCategoryById(int id);
    Task <ResponseDto<object>>AddCategory(CreateCategoryDto createCategoryDto);
    Task <ResponseDto<object>>UpdateCategory(UpdateCategoryDto updateCategoryDto);
    Task <ResponseDto<object>>DeleteCategory(int id);
}