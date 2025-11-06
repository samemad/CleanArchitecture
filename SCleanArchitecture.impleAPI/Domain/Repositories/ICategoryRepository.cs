using SCleanArchitecture.SimpleAPI.Domain.Entities;

namespace SCleanArchitecture.SimpleAPI.Domain.Repositories;

public interface ICategoryRepository
{
    
    Task<Category> GetCategoryByIdAsync(int id);
    Task<IEnumerable<Category>> GetAllCategoriesAsync();
    Task AddCategoryAsync(Category category);
    Task UpdateCategoryAsync(Category category);
    Task DeleteCategoryAsync(int id);

    // Additional Query Methods
    // This will load the category WITH all its products (using Include in EF Core)
    Task<Category> GetCategoryWithProductsAsync(int id);
}