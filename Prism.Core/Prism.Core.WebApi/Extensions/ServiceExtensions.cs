using Prism.Core.WebApi.Mappers;
using Prism.Core.WebApi.Repositories;
using Prism.Core.WebApi.Repositories.Abstractions;
using Prism.Core.WebApi.Services;
using Prism.Core.WebApi.Services.Abstractions;

namespace Prism.Core.WebApi.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IRateRepository, RateRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();

        return services;
    }

    public static IServiceCollection AddMappers(this IServiceCollection services)
    {
        services.AddScoped<AuthMapper>();
        services.AddScoped<RegisterMapper>();
        services.AddScoped<UserMapper>();
        services.AddScoped<RateMapper>();

        return services;
    }
}
