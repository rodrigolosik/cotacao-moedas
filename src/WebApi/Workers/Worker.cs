using Application.Common.Interfaces;
using Domain.Configurations;
using Microsoft.Extensions.Options;
using Serilog;

namespace WebApi.Workers;

public class Worker : BackgroundService
{
    private readonly IEconomiaService _economiaService;
    private readonly ITelegramService _telegramService;
    private readonly WorkerConfiguration _workerConfiguration;

    public Worker(IEconomiaService economiaService, ITelegramService telegramService, IOptions<WorkerConfiguration> options)
    {
        _economiaService = economiaService;
        _telegramService = telegramService;
        _workerConfiguration = options.Value;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                Log.Information($"Iniciando o envio das cotações: {DateTime.Now:dd/MM/yyyy HH:mm:ss}");

                var cotacoes = await _economiaService.PegarCotacoesPorQuantidadeDeDiasAsync();

                Log.Information("Cotações consultadas com sucesso");

                await _telegramService.SendMessageAsync(cotacoes);

                Log.Information($"Próximo envio em: {DateTime.Now.AddMinutes(_workerConfiguration.DelayTimeInMinutes):dd/MM/yyyy HH:mm:ss}");

                await Task.Delay(TimeSpan.FromMinutes(_workerConfiguration.DelayTimeInMinutes), stoppingToken);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
            }
        }
    }
}
