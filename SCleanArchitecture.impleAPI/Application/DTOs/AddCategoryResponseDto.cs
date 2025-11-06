namespace SCleanArchitecture.SimpleAPI.Application.DTOs;

public class AddCategoryResponseDto
{
	public int Id { get; set; }
	public string Name { get; set; }
	public string Description { get; set; }
	public DateTime CreatedAt { get; set; }
	public int ProductCount { get; set; }  // Optional: To show how many products in this category
}