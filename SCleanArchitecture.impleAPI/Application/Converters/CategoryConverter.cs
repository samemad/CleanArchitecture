// CategoryConverter.cs
using SCleanArchitecture.SimpleAPI.Application.DTOs;
using SCleanArchitecture.SimpleAPI.Domain.Entities;

namespace SCleanArchitecture.SimpleAPI.Application.Converters;

internal static class CategoryConverter
{
    // Convert AddCategoryRequestDto → Category Entity
    public static Category ToCategoryEntity(this AddCategoryRequestDto requestDto)
    {
        return new Category
        {
            Name = requestDto.Name,
            Description = requestDto.Description
        };
    }

    // Convert Category Entity → AddCategoryResponseDto
    public static AddCategoryResponseDto ToAddCategoryResponse(this Category category)
    {
        return new AddCategoryResponseDto
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description,
            CreatedAt = category.CreatedAt,
            ProductCount = category.Products?.Count ?? 0  // Count products if loaded
        };
    }

    // Convert UpdateCategoryRequestDto → Category Entity
    public static Category ToCategoryEntity(this UpdateCategoryRequestDto requestDto)
    {
        return new Category
        {
            Id = requestDto.Id,
            Name = requestDto.Name,
            Description = requestDto.Description
        };
    }

    // Convert Category Entity → GetCategoryWithProductsResponseDto
    // (BONUS: Use this when you load category WITH products using GetCategoryWithProductsAsync)
    public static GetCategoryWithProductsResponseDto ToCategoryWithProductsResponse(this Category category)
    {
        return new GetCategoryWithProductsResponseDto
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description,
            CreatedAt = category.CreatedAt,
            Products = category.Products?.Select(p => new ProductInCategoryDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Stock = p.Stock
            }).ToList() ?? new List<ProductInCategoryDto>()
        };
    }
}