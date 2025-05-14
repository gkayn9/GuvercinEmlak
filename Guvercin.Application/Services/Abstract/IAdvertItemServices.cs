using Guvercin.Application.Dtos.AdvertItemsDtos;

namespace Guvercin.Application.Services.Abstract;

public interface IAdvertItemServices
{
    Task<List<ResultAdvertItemDto>>GetAllAdvertItems();
    Task<DetailAdvertItemDto> GetByIdAdvertItem(int id);
    Task AddAdvertItem(CreateAdvertItemDto createAdvertItemDto);
    Task UpdateAdvertItem(UpdateAdvertItemDto updateAdvertItemDto);
    Task DeleteAdvertItem(int id);
}