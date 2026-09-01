using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Prism.Core.WebApi.Mappers;
using Prism.Core.WebApi.Repositories;
using Prism.Core.WebApi.Repositories.Abstractions;
using Prism.Core.WebApi.Services;
using Prism.Core.WebApi.Services.Abstractions;
using System.Text;

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

    public static IServiceCollection AddPrismAuthentication(this IServiceCollection services, string? secretKey)
    {
        if (string.IsNullOrEmpty(secretKey) || secretKey.Length < 32)
            throw new Exception("JWT Secret Key must be at least 32 characters long.");
        var key = Encoding.ASCII.GetBytes(secretKey);

        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

        return services;
    }
}
