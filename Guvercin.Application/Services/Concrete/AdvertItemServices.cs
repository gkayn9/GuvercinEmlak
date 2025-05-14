using AutoMapper;
using Guvercin.Application.Dtos.AdvertItemsDtos;
using Guvercin.Application.Interfaces;
using Guvercin.Application.Services.Abstract;
using Guvercin.Domain.Entities;

namespace Guvercin.Application.Services.Concrete;

public class AdvertItemServices:IAdvertItemServices
{
    private readonly IGenericRepository<AdvertItem> _advertItemRepository;
    private readonly IMapper _mapper;

    public AdvertItemServices(IGenericRepository<AdvertItem> advertItemRepository, IMapper mapper)
    {
        _advertItemRepository = advertItemRepository;
        _mapper = mapper;
    }


    public async Task<List<ResultAdvertItemDto>> GetAllAdvertItems()
    {
        var advertItems = await _advertItemRepository.GetAllAsync();
        var result = _mapper.Map<List<ResultAdvertItemDto>>(advertItems);
        return result;
    }

    public async Task<DetailAdvertItemDto> GetByIdAdvertItem(int id)
    {
        var advertItem = await _advertItemRepository.GetByIdAsync(id);
        var result = _mapper.Map<DetailAdvertItemDto>(advertItem);
        return result;
    }

    public async Task AddAdvertItem(CreateAdvertItemDto createAdvertItemDto)
    {
        var advertItem = _mapper.Map<AdvertItem>(createAdvertItemDto);
        await _advertItemRepository.AddAsync(advertItem);
    }

    public async Task UpdateAdvertItem(UpdateAdvertItemDto updateAdvertItemDto)
    {
        var advertItem= await _advertItemRepository.GetByIdAsync(updateAdvertItemDto.Id);
        var newAdvertItem = _mapper.Map(updateAdvertItemDto,advertItem);
        await _advertItemRepository.UpdateAsync(newAdvertItem);
    }

    public async Task DeleteAdvertItem(int id)
    {
        var advertItem = await _advertItemRepository.GetByIdAsync(id);
        await _advertItemRepository.DeleteAsync(advertItem);

    }
}