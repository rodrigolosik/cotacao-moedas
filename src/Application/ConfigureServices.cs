using Application.Common.Interfaces;
using Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration) {

        services.AddTransient<IEconomiaService, EconomiaService>();
        services.AddTransient<ITelegramService, TelegramService>();
        return services;
    }
}
