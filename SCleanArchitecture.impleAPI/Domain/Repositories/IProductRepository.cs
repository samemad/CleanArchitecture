using SCleanArchitecture.SimpleAPI.Domain.Entities;

namespace SCleanArchitecture.SimpleAPI.Domain.Repositories;
//Repository is implemented in infrastructure layer 
public interface IProductRepository
{
    Task<Product> GetProductByIdAsync(int id);
    Task<IEnumerable<Product>> GetAllProductAsync();
    Task AddProductAsync(Product product);
    Task UpdateProductAsync(Product product);
    Task DeleteProductAsync(int id);

    // Additional Query Methods
    Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(int categoryId);
}