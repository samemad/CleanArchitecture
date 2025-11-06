namespace SCleanArchitecture.SimpleAPI.Application.DTOs;

public class GetCategoryWithProductsResponseDto
{
	public int Id { get; set; }
	public string Name { get; set; }
	public string Description { get; set; }
	public DateTime CreatedAt { get; set; }
	public List<ProductInCategoryDto> Products { get; set; }
}

public class ProductInCategoryDto
{
	public int Id { get; set; }
	public string Name { get; set; }
	public decimal Price { get; set; }
	public int Stock { get; set; }
}
