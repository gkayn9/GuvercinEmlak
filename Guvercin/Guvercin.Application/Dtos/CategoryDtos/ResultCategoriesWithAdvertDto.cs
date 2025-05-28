using Guvercin.Application.Dtos.AdvertItemsDtos;

namespace Guvercin.Application.Dtos.CategoryDtos;

public class ResultCategoriesWithAdvertDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<CategoriesAdvertItemDto> AdvertItems { get; set; }
}