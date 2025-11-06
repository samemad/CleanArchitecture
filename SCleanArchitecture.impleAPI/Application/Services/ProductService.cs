// ProductService.cs
using SCleanArchitecture.SimpleAPI.Application.Converters;
using SCleanArchitecture.SimpleAPI.Application.DTOs;
using SCleanArchitecture.SimpleAPI.Domain.Repositories;

namespace SCleanArchitecture.SimpleAPI.Application.Services;

public interface IProductService
{
    Task<AddProductResponseDto> AddProduct(AddProductRequestDto requestDto);
    Task<List<AddProductResponseDto>> GetAllProducts();
    Task<AddProductResponseDto> GetProductById(int id);
    Task<AddProductResponseDto> UpdateProduct(UpdateProductRequestDto requestDto);
    Task<bool> DeleteProduct(int id);
    Task<List<AddProductResponseDto>> GetProductsByCategoryId(int categoryId);
}

internal sealed class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;  // To validate CategoryId exists

    public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<AddProductResponseDto> AddProduct(AddProductRequestDto requestDto)
    {
        // Validate request
        if (!requestDto.IsValid())
        {
            return ProductErrors.InvalidRequest();
        }

        // Check if category exists
        var category = await _categoryRepository.GetCategoryByIdAsync(requestDto.CategoryId);
        if (category == null)
        {
            return ProductErrors.CategoryNotFound();
        }

        // Convert DTO to Entity
        var productEntity = requestDto.ToProductEntity();

        // Save to database
        await _productRepository.AddProductAsync(productEntity);

        // Get the saved product with Category info
        var savedProduct = await _productRepository.GetProductByIdAsync(productEntity.Id);

        // Convert to response DTO
        var response = savedProduct.ToAddProductResponse();

        return response;

        // ✅ NO TRY-CATCH! Let exceptions bubble up naturally
    }

    public async Task<List<AddProductResponseDto>> GetAllProducts()
    {
        // Get all products from repository
        var products = await _productRepository.GetAllProductAsync();

        // Convert each Product entity to AddProductResponseDto
        var response = products.Select(p => p.ToAddProductResponse()).ToList();

        return response;
    }

    public async Task<AddProductResponseDto> GetProductById(int id)
    {
        // Get product from repository
        var product = await _productRepository.GetProductByIdAsync(id);

        // If product not found, return null
        if (product == null)
            return null;

        // Convert to response DTO
        var response = product.ToAddProductResponse();

        return response;
    }

    public async Task<AddProductResponseDto> UpdateProduct(UpdateProductRequestDto requestDto)
    {
        // Validate request
        if (!requestDto.IsValid())
        {
            return ProductErrors.InvalidRequest();
        }

        // Check if product exists
        var existingProduct = await _productRepository.GetProductByIdAsync(requestDto.Id);
        if (existingProduct == null)
            return null;

        // Check if new category exists
        var category = await _categoryRepository.GetCategoryByIdAsync(requestDto.CategoryId);
        if (category == null)
        {
            return ProductErrors.CategoryNotFound();
        }

        // Convert DTO to Entity
        var productEntity = requestDto.ToProductEntity();

        // Update product in repository
        await _productRepository.UpdateProductAsync(productEntity);

        // Get the updated product to return
        var updatedProduct = await _productRepository.GetProductByIdAsync(requestDto.Id);

        // Convert to response DTO
        var response = updatedProduct.ToAddProductResponse();

        return response;
    }

    public async Task<bool> DeleteProduct(int id)
    {
        // Check if product exists
        var product = await _productRepository.GetProductByIdAsync(id);
        if (product == null)
            return false;

        // Delete product
        await _productRepository.DeleteProductAsync(id);

        return true;
    }

    public async Task<List<AddProductResponseDto>> GetProductsByCategoryId(int categoryId)
    {
        // Get all products in this category
        var products = await _productRepository.GetProductsByCategoryIdAsync(categoryId);

        // Convert to response DTOs
        var response = products.Select(p => p.ToAddProductResponse()).ToList();

        return response;
    }
}

// Error handling helper
public static class ProductErrors
{
    public static AddProductResponseDto InvalidRequest()
    {
        return new AddProductResponseDto();
    }

    public static AddProductResponseDto CategoryNotFound()
    {
        return new AddProductResponseDto();
    }
}