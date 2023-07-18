using Domain.Configurations;
using WebApi.Workers;

namespace WebApi;

public static class ConfigureServices
{
    public static IServiceCollection AddWebApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<TelegramConfiguration>()
            .BindConfiguration(nameof(TelegramConfiguration));

        services.AddOptions<WorkerConfiguration>()
            .BindConfiguration(nameof(WorkerConfiguration));

        services.AddOptions<EconomiaConfiguration>()
            .BindConfiguration(nameof(EconomiaConfiguration));

        services.AddHostedService<Worker>();

        return services;
    }
}
