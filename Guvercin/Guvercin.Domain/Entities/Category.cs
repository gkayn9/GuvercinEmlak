namespace Guvercin.Domain.Entities;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<AdvertItem>AdvertItems { get; set; }
}