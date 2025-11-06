namespace SCleanArchitecture.SimpleAPI.Domain.Entities;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    // Navigation Property (allows you to access all products in this category)
    public ICollection<Product> Products { get; set; }

    public DateTime CreatedAt { get; set; }
}