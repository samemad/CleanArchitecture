using SCleanArchitecture.SimpleAPI.Domain.Entities;
using SCleanArchitecture.SimpleAPI.Domain.Repositories;

namespace SCleanArchitecture.SimpleAPI.Infrastructure.Repositories;

internal sealed class UserRepository : IUserRepository
{
    // I Made it static so data persists between calls and not desappear after method ends!
    private static List<User> userList = new List<User>();

    public async Task AddUserAsync(User user)
    {
        //For Auto-generate ID
        user.Id = userList.Count > 0 ? userList.Max(u => u.Id) + 1 : 1;

        userList.Add(user);

        await Task.CompletedTask;
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        // Return all users!!
        return await Task.FromResult(userList.AsEnumerable());
    }

    public async Task<User> GetUserByIdAsync(int id)
    {
        // Find user by ID!!
        var user = userList.FirstOrDefault(u => u.Id == id);
        return await Task.FromResult(user);
    }

    public async Task UpdateUserAsync(User user)
    {
        var existingUser = userList.FirstOrDefault(u => u.Id == user.Id);

        if (existingUser != null)
        {
            existingUser.Name = user.Name;
            existingUser.Email = user.Email;
        }

        await Task.CompletedTask;
    }

    public async Task DeleteUserAsync(int id)
    {
        var user = userList.FirstOrDefault(u => u.Id == id);

        if (user != null)
        {
            userList.Remove(user);
        }

        await Task.CompletedTask;
    }
}