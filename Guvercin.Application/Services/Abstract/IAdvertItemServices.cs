using Guvercin.Application.Dtos.AdvertItemsDtos;
using Guvercin.Application.Dtos.ResponseDtos;

namespace Guvercin.Application.Services.Abstract;

public interface IAdvertItemServices
{
    Task<ResponseDto<List<ResultAdvertItemDto>>>GetAllAdvertItems();
    Task<ResponseDto<DetailAdvertItemDto>> GetByIdAdvertItem(int id);
    Task<ResponseDto<object>> AddAdvertItem(CreateAdvertItemDto createAdvertItemDto);
    Task<ResponseDto<object>> UpdateAdvertItem(UpdateAdvertItemDto updateAdvertItemDto);
    Task<ResponseDto<object>>  DeleteAdvertItem(int id);
}