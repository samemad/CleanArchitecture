using Microsoft.EntityFrameworkCore;
using SCleanArchitecture.SimpleAPI.Domain.Entities;
using SCleanArchitecture.SimpleAPI.Domain.Repositories;
using SCleanArchitecture.SimpleAPI.Infrastructure.Data;

namespace SCleanArchitecture.SimpleAPI.Infrastructure.Repositories;

// SOLID: Single Responsibility - This class ONLY handles Category data access
// SOLID: Dependency Inversion - Implements ICategoryRepository interface from Domain
internal sealed class CategoryRepository : ICategoryRepository
{
    private readonly ApplicationDbContext _context;

    // Constructor - receives database context via Dependency Injection
    public CategoryRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddCategoryAsync(Category category)
    {
        // Set CreatedAt timestamp
        category.CreatedAt = DateTime.UtcNow;

        // Add to database (in memory, not saved yet)
        await _context.Categories.AddAsync(category);

        // Save changes to database (writes to SQL Server)
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
    {
        // SELECT * FROM Categories
        return await _context.Categories.ToListAsync();
    }

    public async Task<Category> GetCategoryByIdAsync(int id)
    {
        // SELECT * FROM Categories WHERE Id = @id
        // Does NOT include Products (lighter query)
        return await _context.Categories
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task UpdateCategoryAsync(Category category)
    {
        // Find existing category in database
        var existingCategory = await _context.Categories
            .FirstOrDefaultAsync(c => c.Id == category.Id);

        if (existingCategory != null)
        {
            // Update properties
            existingCategory.Name = category.Name;
            existingCategory.Description = category.Description;

            // Save changes to database
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteCategoryAsync(int id)
    {
        // Find category
        var category = await _context.Categories
            .FirstOrDefaultAsync(c => c.Id == id);

        if (category != null)
        {
            // Remove from database
            _context.Categories.Remove(category);

            // Save changes
            await _context.SaveChangesAsync();
        }
    }

    // Additional Query: Get category WITH all its products
    public async Task<Category> GetCategoryWithProductsAsync(int id)
    {
        // SELECT * FROM Categories 
        // INNER JOIN Products ON Categories.Id = Products.CategoryId
        // WHERE Categories.Id = @id
        return await _context.Categories
            .Include(c => c.Products)  // Eager loading - loads ALL products in this category
            .FirstOrDefaultAsync(c => c.Id == id);
    }
}