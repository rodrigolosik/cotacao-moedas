using Domain.Dtos;

namespace Application.Common.Interfaces;

public interface IEconomiaService
{
    Task<IEnumerable<ResponseEconomiaDto>> PegarCotacoesPorQuantidadeDeDiasAsync();
}