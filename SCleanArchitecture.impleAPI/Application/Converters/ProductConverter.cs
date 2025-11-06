// ProductConverter.cs
using SCleanArchitecture.SimpleAPI.Application.DTOs;
using SCleanArchitecture.SimpleAPI.Domain.Entities;

namespace SCleanArchitecture.SimpleAPI.Application.Converters;

internal static class ProductConverter
{
    // Convert AddProductRequestDto → Product Entity
    public static Product ToProductEntity(this AddProductRequestDto requestDto)
    {
        return new Product
        {
            Name = requestDto.Name,
            Description = requestDto.Description,
            Price = requestDto.Price,
            Stock = requestDto.Stock,
            CategoryId = requestDto.CategoryId
        };
    }

    // Convert Product Entity → AddProductResponseDto
    public static AddProductResponseDto ToAddProductResponse(this Product product)
    {
        return new AddProductResponseDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Stock = product.Stock,
            CategoryId = product.CategoryId,
            CategoryName = product.Category?.Name,  // Safe navigation - might be null
            CreatedAt = product.CreatedAt
        };
    }

    // Convert UpdateProductRequestDto → Product Entity
    public static Product ToProductEntity(this UpdateProductRequestDto requestDto)
    {
        return new Product
        {
            Id = requestDto.Id,
            Name = requestDto.Name,
            Description = requestDto.Description,
            Price = requestDto.Price,
            Stock = requestDto.Stock,
            CategoryId = requestDto.CategoryId
        };
    }