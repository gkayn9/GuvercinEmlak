using AutoMapper;
using FluentValidation;
using Guvercin.Application.Dtos.CategoryDtos;
using Guvercin.Application.Dtos.ResponseDtos;
using Guvercin.Application.Interfaces;
using Guvercin.Application.Services.Abstract;
using Guvercin.Domain.Entities;

namespace Guvercin.Application.Services.Concrete;

public class CategoryServices:ICategoryServices
{
    private readonly IGenericRepository<Category> _categoryRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateCategoryDto> _createCategoryValidator;
    private readonly IValidator<UpdateCategoryDto> _updateCategoryValidator;

    public CategoryServices(IGenericRepository<Category> categoryRepository, IMapper mapper, IValidator<CreateCategoryDto> createCategoryValidator, IValidator<UpdateCategoryDto> updateCategoryValidator)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
        _createCategoryValidator = createCategoryValidator;
        _updateCategoryValidator = updateCategoryValidator;
    }

    public async Task<ResponseDto<List<ResultCategoryDto>>> GetAllCategories()
    {
        try
        {
            var catagories=await _categoryRepository.GetAllAsync();
            if (catagories.Count == 0)
            {
                return new ResponseDto<List<ResultCategoryDto>>
                {
                    Success = false,
                    Message = "No categories found",
                    ErrorCodes = ErrorCodes.NotFound
                };
            }
            var result = _mapper.Map<List<ResultCategoryDto>>(catagories);
            return new ResponseDto<List<ResultCategoryDto>>{
                Success = true,
                Data = result
            };
        }
        catch (Exception e)
        {
            return new ResponseDto<List<ResultCategoryDto>>
            {
                Success = false,
                Message = e.Message,
                ErrorCodes = ErrorCodes.Exception
            };
        }
        
        
        
       
    }

    public async Task<ResponseDto<DetailCategoryDto>>GetCategoryById(int id)
    {
        try
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return new ResponseDto<DetailCategoryDto>
                {
                    Success = false,
                    Message = "Category not found",
                    ErrorCodes = ErrorCodes.NotFound
                };

            }
            
            var result = _mapper.Map<DetailCategoryDto>(category);
            return new ResponseDto<DetailCategoryDto>{
                Success = true,
                Data = result
            };
        }
        catch (Exception e)
        {
            return new ResponseDto<DetailCategoryDto>{Success = false,
                Message = e.Message,
                ErrorCodes = ErrorCodes.Exception
            };
            
        }
        
        
    }

    public async Task<ResponseDto<object>> AddCategory(CreateCategoryDto createCategoryDto)
    {
        try
        {
            
        var validate=await _createCategoryValidator.ValidateAsync(createCategoryDto);
        if (!validate.IsValid)
        {
            return new ResponseDto<object>
            {
                Success = false, 
                Data = null, 
                Message = string.Join(",", validate.Errors.Select(x => x.ErrorMessage)),
                ErrorCodes = ErrorCodes.ValidationError
            };
        }
        var category = _mapper.Map<Category>(createCategoryDto);
                await _categoryRepository.AddAsync(category);
                return new ResponseDto<object>
                {
                    Success = true,
                    Data = null,
                    Message = "Category created successfully"
                };
        }
        catch (Exception e)
        {
            return new ResponseDto<object>
            {
                Success = false,
                Data = null,
                Message = "Something went wrong",
                ErrorCodes = ErrorCodes.Exception
            };
        }
        
    }

    public async Task<ResponseDto<object>> UpdateCategory(UpdateCategoryDto updateCategoryDto)
    {
        try
        {
            var validate=await _updateCategoryValidator.ValidateAsync(updateCategoryDto);
            if (!validate.IsValid)
            {
                return new ResponseDto<object>
                {
                    Success = false, Data = null,
                    Message = string.Join(",", validate.Errors.Select(x => x.ErrorMessage)),
                    ErrorCodes = ErrorCodes.ValidationError
                };
            }
            var categorydb =await _categoryRepository.GetByIdAsync(updateCategoryDto.Id);
            if (categorydb == null)
            {
                return new ResponseDto<object>{
                    Success = false,
                    Message = "Category not found",
                    ErrorCodes = ErrorCodes.NotFound
                };
            }
            var category = _mapper.Map(updateCategoryDto,categorydb);
            await _categoryRepository.UpdateAsync(category);
                    return new ResponseDto<object>{
                        Success = true,
                        Data = null,
                        Message = "Category updated successfully"
                    };
        }
        catch (Exception e)
        {
            return new ResponseDto<object>{
                Success = false,
                Message = e.Message,
                ErrorCodes = ErrorCodes.Exception
            };
        }
        
        
    }

    public async Task<ResponseDto<object>> DeleteCategory(int id)
    {

        try
        {
            var category =await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return new ResponseDto<object>{Success = false,Message = "Category not found", ErrorCodes = ErrorCodes.NotFound };
            
            }
            await _categoryRepository.DeleteAsync(category);
            return new ResponseDto<object>{Success = true,Data = null,Message = "Category deleted successfully"};
        }
        catch (Exception e)
        {
            return new ResponseDto<object>{ Success = false, Message = e.Message, ErrorCodes = ErrorCodes.Exception };
        }
        
        
    }
}
