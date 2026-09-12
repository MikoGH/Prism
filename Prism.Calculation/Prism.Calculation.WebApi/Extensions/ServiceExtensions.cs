using Prism.Calculation.WebApi.Models.Options;
using Prism.Calculation.WebApi.Services;

namespace Prism.Calculation.WebApi.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddHostedService<CalculationRequestConsumerService>();

        return services;
    }

    public static IServiceCollection AddOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<KafkaOptions>(configuration.GetSection("Kafka"));

        return services;
    }
}
