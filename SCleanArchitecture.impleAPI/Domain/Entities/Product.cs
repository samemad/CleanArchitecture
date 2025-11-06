namespace SCleanArchitecture.SimpleAPI.Domain.Entities;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }  // 💰 Products need prices!
    public int Stock { get; set; }

    // Foreign Key
    public int CategoryId { get; set; }

    // Navigation Property (allows you to access Category object)
    public Category Category { get; set; }

    public DateTime CreatedAt { get; set; }
}