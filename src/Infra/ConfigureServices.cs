using Infra.Gateways;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using Serilog;

namespace Infra;

public static class ConfigureServices
{
    public static IServiceCollection AddAInfraServices(this IServiceCollection services, IConfiguration configuration)
    {
        var baseUrls = GetBaseUrlRefitClients(configuration);

        services.AddRefitClient<IEconomiaGateway>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(baseUrls["Economia"]));

        string telegramBaseUrl = GenerateTelegramBaseUrl(configuration, baseUrls["Telegram"]);

        services.AddRefitClient<ITelegramGateway>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(telegramBaseUrl));

        Log.Logger = new LoggerConfiguration()
               .MinimumLevel.Debug()
               .WriteTo.Console()
               .CreateLogger();

        return services;
    }

    private static Dictionary<string, string?> GetBaseUrlRefitClients(IConfiguration configuration) =>
        configuration.GetSection("RefitConfiguration")
            .GetChildren()
            .ToDictionary(k => k.Key, v => v.Value);

    private static string GenerateTelegramBaseUrl(IConfiguration configuration, string baseUrl)
    {
        var apiToken = configuration.GetSection("TelegramConfiguration:ApiToken").Value;
        return baseUrl.Replace("API_TOKEN", apiToken);
    }
}

