using Guvercin.Application.Dtos.CategoryDtos;

namespace Guvercin.Application.Dtos.AdvertItemsDtos;

public class DetailAdvertItemDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string CurrencySymbol { get; set; }
    public string BaseImageUrl { get; set; }
    public string ImageUrl { get; set; }
    public int CategoryId { get; set; }
    public ResultCategoryDto Category { get; set; }
}