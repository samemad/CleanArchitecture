using SCleanArchitecture.SimpleAPI.Domain.Repositories;

namespace SCleanArchitecture.SimpleAPI.Infrastructure.Repositories;

internal static class UserRepositoryExtentions
{
    
    public static IServiceCollection AddCategoryRepsitory(this IServiceCollection services)
    {

        services.AddScoped<ICategoryRepository, CategoryRepository>();


        return services;
    }

}
