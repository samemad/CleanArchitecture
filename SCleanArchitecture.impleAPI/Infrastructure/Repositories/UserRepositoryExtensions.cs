using SCleanArchitecture.SimpleAPI.Domain.Repositories;

namespace SCleanArchitecture.SimpleAPI.Infrastructure.Repositories;

internal static class UserRepositoryExtentions
{
    public static IServiceCollection AddUserRepsitory(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
       

        return services;
    }



}
