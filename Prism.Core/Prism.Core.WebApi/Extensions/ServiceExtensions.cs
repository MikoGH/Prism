using Prism.Core.DataAccess.Repositories;
using Prism.Core.Domain.Contracts;
using Prism.Core.WebApi.Mappers;
using Prism.Core.WebApi.Services;
using Prism.Core.WebApi.Services.Contracts;
using Prism.Core.WebApi.Validators;
using Prism.Core.WebApi.Validators.Contracts;

namespace Prism.Core.WebApi.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IRateRepository, RateRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IThemeRepository, ThemeRepository>();
        services.AddScoped<IThemeVersionRepository, ThemeVersionRepository>();
        services.AddScoped<IThemeFieldRepository, ThemeFieldRepository>();
        services.AddScoped<IThemeFieldVersionRepository, ThemeFieldVersionRepository>();

        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IThemeValidator, ThemeValidator>();
        services.AddScoped<IThemeFieldValidator, ThemeFieldValidator>();
        services.AddScoped<IThemeService, ThemeService>();
        services.AddScoped<IThemeFieldService, ThemeFieldService>();

        return services;
    }

    public static IServiceCollection AddMappers(this IServiceCollection services)
    {
        services.AddScoped<AuthMapper>();
        services.AddScoped<RegisterMapper>();
        services.AddScoped<UserMapper>();
        services.AddScoped<RateMapper>();
        services.AddScoped<ThemeMapper>();

        return services;
    }
}
