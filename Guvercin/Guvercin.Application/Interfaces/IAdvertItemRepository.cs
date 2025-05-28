using Guvercin.Application.Dtos.AdvertItemsDtos;
using Guvercin.Domain.Entities;

namespace Guvercin.Application.Interfaces;

public interface IAdvertItemRepository
{
    Task<List<AdvertItem>> GetAdvertItemFilterByCategoryId(int categoryId);
}