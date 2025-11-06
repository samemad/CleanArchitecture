using SCleanArchitecture.SimpleAPI.Application.Converters;

namespace SCleanArchitecture.SimpleAPI.Application.Extensions;

public static class AddDependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddService();

        return services;
    }
}
