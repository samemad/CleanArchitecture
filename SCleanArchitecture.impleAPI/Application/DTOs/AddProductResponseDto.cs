namespace SCleanArchitecture.SimpleAPI.Application.DTOs;

public class AddProductResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }  // To show category name in response
    public DateTime CreatedAt { get; set; }
}
