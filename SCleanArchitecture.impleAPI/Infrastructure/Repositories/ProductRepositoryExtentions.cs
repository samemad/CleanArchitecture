using SCleanArchitecture.SimpleAPI.Domain.Repositories;

namespace SCleanArchitecture.SimpleAPI.Infrastructure.Repositories;

internal static class UserRepositoryExtentions
{
 

    public static IServiceCollection AddProductRepsitory(this IServiceCollection services)
    {

        services.AddScoped<IProductRepository, ProductRepository>();


        return services;
    }



}
