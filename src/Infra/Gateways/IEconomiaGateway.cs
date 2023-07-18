using Domain.Dtos;
using Refit;

namespace Infra.Gateways;

public interface IEconomiaGateway
{
    [Get("/json/last/{paridade}")]
    Task<ResponseEconomiaDto> PegarCotacaoAsync(string paridade);

    [Get("/json/daily/{moeda}/{intervalo_dias}")]
    Task<IEnumerable<ResponseEconomiaDto>> PegarCotacoesPorQuantidadeDeDiasAsync(string moeda, int intervalo_dias);
}
