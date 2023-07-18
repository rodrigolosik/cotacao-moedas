using Application.Common.Interfaces;
using Domain.Configurations;
using Domain.Dtos;
using Infra.Gateways;
using Microsoft.Extensions.Options;
using Serilog;

namespace Application.Services;

public class EconomiaService : IEconomiaService
{
    private readonly IEconomiaGateway _economiaGateway;
    private readonly EconomiaConfiguration _economiaConfiguration;

    public EconomiaService(IEconomiaGateway economiaGateway, IOptions<EconomiaConfiguration> options)
    {
        _economiaGateway = economiaGateway;
        _economiaConfiguration = options.Value;
    }

    public async Task<IEnumerable<ResponseEconomiaDto>> PegarCotacoesPorQuantidadeDeDiasAsync()
    {
        try
        {
            Log.Information($"Iniciando a consulta das cotações do {_economiaConfiguration.Moeda} para o período de {_economiaConfiguration.IntervaloDeDias}");

            return await _economiaGateway.PegarCotacoesPorQuantidadeDeDiasAsync(_economiaConfiguration.Moeda, _economiaConfiguration.IntervaloDeDias);
        }
        catch (Exception ex)
        {
            Log.Error(ex, ex.Message);
            throw;
        }
    }
}
