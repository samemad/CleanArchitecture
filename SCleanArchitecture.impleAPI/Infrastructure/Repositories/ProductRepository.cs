using Microsoft.EntityFrameworkCore;
using SCleanArchitecture.SimpleAPI.Domain.Entities;
using SCleanArchitecture.SimpleAPI.Domain.Repositories;
using SCleanArchitecture.SimpleAPI.Infrastructure.Data;

namespace SCleanArchitecture.SimpleAPI.Infrastructure.Repositories;

// SOLID: Single Responsibility - This class ONLY handles Product data access
// SOLID: Dependency Inversion - Implements IProductRepository interface from Domain
internal sealed class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;

    // Constructor - receives database context via Dependency Injection
    public ProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddProductAsync(Product product)
    {
        // Set CreatedAt timestamp
        product.CreatedAt = DateTime.UtcNow;

        // Add to database (in memory, not saved yet)
        await _context.Products.AddAsync(product);

        // Save changes to database (writes to SQL Server)
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        // SELECT * FROM Products
        // Include Category so we can see which category each product belongs to
        return await _context.Products
            .Include(p => p.Category)  // Eager loading - loads Category data too
            .ToListAsync();
    }

    public async Task<Product> GetProductByIdAsync(int id)
    {
        // SELECT * FROM Products WHERE Id = @id
        // Include Category navigation property
        return await _context.Products
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task UpdateProductAsync(Product product)
    {
        // Find existing product in database
        var existingProduct = await _context.Products
            .FirstOrDefaultAsync(p => p.Id == product.Id);

        if (existingProduct != null)
        {
            // Update properties
            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;
            existingProduct.Stock = product.Stock;
            existingProduct.CategoryId = product.CategoryId;

            // Save changes to database
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteProductAsync(int id)
    {
        // Find product
        var product = await _context.Products
            .FirstOrDefaultAsync(p => p.Id == id);

        if (product != null)
        {
            // Remove from database
            _context.Products.Remove(product);

            // Save changes
            await _context.SaveChangesAsync();
        }
    }

    // Additional Query: Get all products in a specific category
    public async Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(int categoryId)
    {
        // SELECT * FROM Products WHERE CategoryId = @categoryId
        return await _context.Products
            .Include(p => p.Category)
            .Where(p => p.CategoryId == categoryId)
            .ToListAsync();
    }
}