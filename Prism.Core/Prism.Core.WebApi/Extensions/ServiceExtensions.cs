using Prism.Core.WebApi.Repositories;
using Prism.Core.WebApi.Repositories.Abstractions;

namespace Prism.Core.WebApi.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IRateRepository, RateRepository>();

        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services;
    }
}
