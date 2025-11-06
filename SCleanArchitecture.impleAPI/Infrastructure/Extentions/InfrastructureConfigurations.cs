using SCleanArchitecture.SimpleAPI.Infrastructure.Repositories;

namespace SCleanArchitecture.SimpleAPI.Infrastructure.Extentions;

public static class InfrastructureConfigurations
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddUserRepsitory();
        services.AddProductRepsitory();
        services.AddCategoryRepsitory();

        return services;
    }
}