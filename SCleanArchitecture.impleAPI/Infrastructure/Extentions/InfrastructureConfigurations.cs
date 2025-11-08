using SCleanArchitecture.SimpleAPI.Infrastructure.Repositories;

namespace SCleanArchitecture.SimpleAPI.Infrastructure.Extentions;

public static class InfrastructureConfigurations
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddUserRepository();
        services.AddProductRepository();
        services.AddCategoryRepository();
        services.AddOrderRepository();  // ⚡ MAKE SURE THIS IS HERE!

        return services;
    }
}