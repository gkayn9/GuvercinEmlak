using AutoMapper;
using FluentValidation;
using Guvercin.Application.Dtos.AdvertItemsDtos;
using Guvercin.Application.Dtos.ResponseDtos;
using Guvercin.Application.Interfaces;
using Guvercin.Application.Services.Abstract;
using Guvercin.Domain.Entities;

namespace Guvercin.Application.Services.Concrete;

public class AdvertItemServices:IAdvertItemServices
{
    private readonly IGenericRepository<AdvertItem> _advertItemRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateAdvertItemDto> _validator;
    private readonly IValidator<UpdateAdvertItemDto> _updateValidator;

    public AdvertItemServices(IGenericRepository<AdvertItem> advertItemRepository, IMapper mapper, IValidator<CreateAdvertItemDto> validator, IValidator<UpdateAdvertItemDto> updateValidator)
    {
        _advertItemRepository = advertItemRepository;
        _mapper = mapper;
        _validator = validator;
        _updateValidator = updateValidator;
    }


    public async Task<ResponseDto<List<ResultAdvertItemDto>>>GetAllAdvertItems()
    {
        try
        {
 
            var advertItems = await _advertItemRepository.GetAllAsync();
            if (advertItems.Count==0)
            {
                return new ResponseDto<List<ResultAdvertItemDto>>{Success = false ,Data = null ,Message = "İlan Bulunamadı",ErrorCodes = ErrorCodes.NotFound};
            
            }
        
            var result = _mapper.Map<List<ResultAdvertItemDto>>(advertItems);
            return new ResponseDto<List<ResultAdvertItemDto>>{
                Success = true,
                Data = result
            };
        }
        catch (Exception e)
        {
            return new ResponseDto<List<ResultAdvertItemDto>>
            {
                Success = false,
                Data = null,
                Message = "Bir hata oluştu",
                ErrorCodes = ErrorCodes.Exception
            };
        }
        
    }

    public async Task<ResponseDto<DetailAdvertItemDto>> GetByIdAdvertItem(int id)
    {
        var advertItem = await _advertItemRepository.GetByIdAsync(id);
        if (advertItem == null)
        {
            return new ResponseDto<DetailAdvertItemDto>{Success = false,Data = null,Message = "İlan Bulunamadı",ErrorCodes = ErrorCodes.NotFound};
        }
        var result = _mapper.Map<DetailAdvertItemDto>(advertItem);
        return new ResponseDto<DetailAdvertItemDto>{Success = true,Data = result};
    }

    public async Task<ResponseDto<object>>  AddAdvertItem(CreateAdvertItemDto createAdvertItemDto)
    {
        try
        {
            var validate= await _validator.ValidateAsync(createAdvertItemDto);
            if (!validate.IsValid)
            {
                return new ResponseDto<object>
                {
                    Success = false,
                    Data = null,
                    Message = string.Join(',', validate.Errors.Select(x => x.ErrorMessage).ToList()),
                    ErrorCodes = ErrorCodes.ValidationError
                };
            }
            var advertItem = _mapper.Map<AdvertItem>(createAdvertItemDto);
            await _advertItemRepository.AddAsync(advertItem);
            return new ResponseDto<object>
            {
                Success = true,
                Data = null,
                Message = "İlan Eklendi"
            };
        }
        catch (Exception e)
        {
            return new ResponseDto<object>
            {
                Success = false,
                Data = null,
                Message = "Bir hata oluştu",
                ErrorCodes = ErrorCodes.Exception
            };
        }
        
    }

    public async Task<ResponseDto<object>> UpdateAdvertItem(UpdateAdvertItemDto updateAdvertItemDto)
    {
        try
        {
            var validate = await _updateValidator.ValidateAsync(updateAdvertItemDto);
            if (!validate.IsValid)
            {
                return new ResponseDto<object>
                {
                    Success = false,
                    Data = null,
                    Message = string.Join(',', validate.Errors.Select(x => x.ErrorMessage).ToList()),
                    ErrorCodes = ErrorCodes.ValidationError
                };
            }
            var advertItem= await _advertItemRepository.GetByIdAsync(updateAdvertItemDto.Id);
            if (advertItem == null)
            {
                return new ResponseDto<object>
                {
                    Success = false,
                    Data = null,
                    Message = "İlan Bulunamadı",
                    ErrorCodes = ErrorCodes.NotFound
                };
                
            }
            var newAdvertItem = _mapper.Map(updateAdvertItemDto,advertItem);
            await _advertItemRepository.UpdateAsync(newAdvertItem);
            return new ResponseDto<object>
            {
                Success = true,
                Data = null,
                Message = "İlan Güncellendi"
            };
        }
        catch (Exception e)
        {
            return new ResponseDto<object>
            {
                Success = false,
                Data = null,
                Message = "Bir hata oluştu",
                ErrorCodes = ErrorCodes.Exception
            };
        }
        
    }

    public async Task<ResponseDto<object>>  DeleteAdvertItem(int id)
    {
        try
        {
            var advertItem = await _advertItemRepository.GetByIdAsync(id);
            if (advertItem == null)
            {
                return new ResponseDto<object>
                {
                    Success = false,
                    Data = null,
                    Message = "İlan Bulunamadı",
                    ErrorCodes = ErrorCodes.NotFound
                };
            }
            await _advertItemRepository.DeleteAsync(advertItem);
            return new ResponseDto<object>
            {
                Success = true,
                Data = null,
                Message = "İlan Silindi"
            };
        }
        catch (Exception e)
        {
            return new ResponseDto<object>
            {
                Success = false,
                Data = null,
                Message = "Bir hata oluştu",
                ErrorCodes = ErrorCodes.Exception
            };
        }
        

    }
}