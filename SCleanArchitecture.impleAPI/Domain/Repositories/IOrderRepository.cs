using SCleanArchitecture.SimpleAPI.Domain.Entities;

namespace SCleanArchitecture.SimpleAPI.Domain.Repositories;

public interface IOrderRepository
{
    Task<Order> GetOrderByIdAsync(int id);
    Task<Order> GetOrderWithItemsAsync(int id);  // Include OrderItems
    Task<IEnumerable<Order>> GetAllOrdersAsync();
    Task<IEnumerable<Order>> GetAllOrdersWithItemsAsync();  // Include all items
    Task AddOrderAsync(Order order);
    Task UpdateOrderAsync(Order order);
    Task DeleteOrderAsync(int id);

    // Additional queries
    Task<IEnumerable<Order>> GetOrdersByCustomerEmailAsync(string email);
    Task<IEnumerable<Order>> GetOrdersByStatusAsync(string status);
}