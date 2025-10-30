using SCleanArchitecture.SimpleAPI.Domain.Entities;
using SCleanArchitecture.SimpleAPI.Domain.Repositories;

namespace SCleanArchitecture.SimpleAPI.Infrastructure.Repositories;

internal sealed class UserRepository : IUserRepository
{

    public async Task AddUserAsync(User user)
    {
       var userList = new List<User>(); // temporary db table , we will configure database next day

        userList.Add(user);  // add new user to the table 

        await Task.CompletedTask;
    }

    public Task DeleteUserAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<User>> GetAllUsersAsync()
    {
        throw new NotImplementedException();
    }

    public Task<User> GetUserByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateUserAsync(User user)
    {
        throw new NotImplementedException();
    }
}
