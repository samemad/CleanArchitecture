using Microsoft.EntityFrameworkCore;
using SCleanArchitecture.SimpleAPI.Domain.Entities;
using SCleanArchitecture.SimpleAPI.Domain.Repositories;
using SCleanArchitecture.SimpleAPI.Infrastructure.Data;

namespace SCleanArchitecture.SimpleAPI.Infrastructure.Repositories;

internal sealed class OrderRepository : IOrderRepository
{
    private readonly ApplicationDbContext _context;

    public OrderRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddOrderAsync(Order order)
    {
        order.CreatedAt = DateTime.UtcNow;
        order.Status = "Pending";  // Default status

        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Order>> GetAllOrdersAsync()
    {
        return await _context.Orders.ToListAsync();
    }

    public async Task<IEnumerable<Order>> GetAllOrdersWithItemsAsync()
    {
        return await _context.Orders
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)  // Include Product details in each item
            .ToListAsync();
    }

    public async Task<Order> GetOrderByIdAsync(int id)
    {
        return await _context.Orders
            .FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task<Order> GetOrderWithItemsAsync(int id)
    {
        return await _context.Orders
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
            .FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task UpdateOrderAsync(Order order)
    {
        var existingOrder = await _context.Orders
            .FirstOrDefaultAsync(o => o.Id == order.Id);

        if (existingOrder != null)
        {
            existingOrder.Status = order.Status;
            existingOrder.CustomerName = order.CustomerName;
            existingOrder.CustomerEmail = order.CustomerEmail;
            existingOrder.TotalAmount = order.TotalAmount;

            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteOrderAsync(int id)
    {
        var order = await _context.Orders
            .Include(o => o.OrderItems)  // Include items to delete them too
            .FirstOrDefaultAsync(o => o.Id == id);

        if (order != null)
        {
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Order>> GetOrdersByCustomerEmailAsync(string email)
    {
        return await _context.Orders
            .Include(o => o.OrderItems)
            .Where(o => o.CustomerEmail == email)
            .ToListAsync();
    }

    public async Task<IEnumerable<Order>> GetOrdersByStatusAsync(string status)
    {
        return await _context.Orders
            .Include(o => o.OrderItems)
            .Where(o => o.Status == status)
            .ToListAsync();
    }
}