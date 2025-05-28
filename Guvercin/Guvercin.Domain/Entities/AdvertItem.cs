namespace Guvercin.Domain.Entities;

public class AdvertItem
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string CurrencySymbol { get; set; }
    public string BaseImageUrl { get; set; }
    public string ImageUrl { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }

}