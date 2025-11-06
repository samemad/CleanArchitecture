// CategoryService.cs
using SCleanArchitecture.SimpleAPI.Application.Converters;
using SCleanArchitecture.SimpleAPI.Application.DTOs;
using SCleanArchitecture.SimpleAPI.Domain.Repositories;

namespace SCleanArchitecture.SimpleAPI.Application.Services;

public interface ICategoryService
{
    Task<AddCategoryResponseDto> AddCategory(AddCategoryRequestDto requestDto);
    Task<List<AddCategoryResponseDto>> GetAllCategories();
    Task<AddCategoryResponseDto> GetCategoryById(int id);
    Task<AddCategoryResponseDto> UpdateCategory(UpdateCategoryRequestDto requestDto);
    Task<bool> DeleteCategory(int id);
    Task<GetCategoryWithProductsResponseDto> GetCategoryWithProducts(int id);
}

internal sealed class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IProductRepository _productRepository;  // To check if category has products before deleting

    public CategoryService(ICategoryRepository categoryRepository, IProductRepository productRepository)
    {
        _categoryRepository = categoryRepository;
        _productRepository = productRepository;
    }

    public async Task<AddCategoryResponseDto> AddCategory(AddCategoryRequestDto requestDto)
    {
        // Validate request
        if (!requestDto.IsValid())
        {
            return CategoryErrors.InvalidRequest();
        }

        // Convert DTO to Entity
        var categoryEntity = requestDto.ToCategoryEntity();

        // Save to database
        await _categoryRepository.AddCategoryAsync(categoryEntity);

        // Get the saved category
        var savedCategory = await _categoryRepository.GetCategoryByIdAsync(categoryEntity.Id);

        // Convert to response DTO
        var response = savedCategory.ToAddCategoryResponse();

        return response;

        // ✅ NO TRY-CATCH! Let exceptions bubble up naturally
    }

    public async Task<List<AddCategoryResponseDto>> GetAllCategories()
    {
        // Get all categories from repository
        var categories = await _categoryRepository.GetAllCategoriesAsync();

        // Convert each Category entity to AddCategoryResponseDto
        var response = categories.Select(c => c.ToAddCategoryResponse()).ToList();

        return response;
    }

    public async Task<AddCategoryResponseDto> GetCategoryById(int id)
    {
        // Get category from repository (without products)
        var category = await _categoryRepository.GetCategoryByIdAsync(id);

        // If category not found, return null
        if (category == null)
            return null;

        // Convert to response DTO
        var response = category.ToAddCategoryResponse();

        return response;
    }

    public async Task<AddCategoryResponseDto> UpdateCategory(UpdateCategoryRequestDto requestDto)
    {
        // Validate request
        if (!requestDto.IsValid())
        {
            return CategoryErrors.InvalidRequest();
        }

        // Check if category exists
        var existingCategory = await _categoryRepository.GetCategoryByIdAsync(requestDto.Id);
        if (existingCategory == null)
            return null;

        // Convert DTO to Entity
        var categoryEntity = requestDto.ToCategoryEntity();

        // Update category in repository
        await _categoryRepository.UpdateCategoryAsync(categoryEntity);

        // Get the updated category to return
        var updatedCategory = await _categoryRepository.GetCategoryByIdAsync(requestDto.Id);

        // Convert to response DTO
        var response = updatedCategory.ToAddCategoryResponse();

        return response;
    }

    public async Task<bool> DeleteCategory(int id)
    {
        // Check if category exists
        var category = await _categoryRepository.GetCategoryByIdAsync(id);
        if (category == null)
            return false;

        // BUSINESS RULE: Check if category has products
        // You might want to prevent deleting categories that have products
        var productsInCategory = await _productRepository.GetProductsByCategoryIdAsync(id);
        if (productsInCategory.Any())
        {
            // Option 1: Prevent deletion if products exist
            throw new InvalidOperationException("Cannot delete category with existing products. Delete products first.");

            // Option 2: Delete all products in category first (commented out - use if you want cascade delete)
            // foreach (var product in productsInCategory)
            // {
            //     await _productRepository.DeleteProductAsync(product.Id);
            // }
        }

        // Delete category
        await _categoryRepository.DeleteCategoryAsync(id);

        return true;
    }

    // BONUS: Get category WITH all its products
    public async Task<GetCategoryWithProductsResponseDto> GetCategoryWithProducts(int id)
    {
        // Use the special repository method that loads products
        var category = await _categoryRepository.GetCategoryWithProductsAsync(id);

        // If category not found, return null
        if (category == null)
            return null;

        // Convert to response DTO (includes all products)
        var response = category.ToCategoryWithProductsResponse();

        return response;
    }
}

// Error handling helper
public static class CategoryErrors
{
    public static AddCategoryResponseDto InvalidRequest()
    {
        return new AddCategoryResponseDto();
    }
}