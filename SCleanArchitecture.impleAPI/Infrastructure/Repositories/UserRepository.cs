// here I replaced List<User> with DATABASE operations

using Microsoft.EntityFrameworkCore;
using SCleanArchitecture.SimpleAPI.Domain.Entities;
using SCleanArchitecture.SimpleAPI.Domain.Repositories;
using SCleanArchitecture.SimpleAPI.Infrastructure.Data;

namespace SCleanArchitecture.SimpleAPI.Infrastructure.Repositories;

internal sealed class UserRepository : IUserRepository
{
    // Instead of static List, I use DbContext (database connection)
    private readonly ApplicationDbContext _context;

    // Constructor - receives database context via Dependency Injection
    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddUserAsync(User user)
    {
        // Set CreatedAt timestamp
        user.CreatedAt = DateTime.UtcNow;

        // Add to database (in memory, not saved yet)
        await _context.Users.AddAsync(user);

        // Save changes to database (writes to SQL Server)
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        // SELECT * FROM Users
        return await _context.Users.ToListAsync();
    }

    public async Task<User> GetUserByIdAsync(int id)
    {
        // SELECT * FROM Users WHERE Id = @id
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task UpdateUserAsync(User user)
    {
        // Find existing user in database
        var existingUser = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == user.Id);

        if (existingUser != null)
        {
            // Update properties
            existingUser.Name = user.Name;
            existingUser.Email = user.Email;

            // Save changes to database
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteUserAsync(int id)
    {
        // Find user
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == id);

        if (user != null)
        {
            // Remove from database
            _context.Users.Remove(user);

            // Save changes
            await _context.SaveChangesAsync();
        }
    }
}