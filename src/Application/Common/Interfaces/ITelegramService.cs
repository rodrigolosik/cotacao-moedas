using Domain.Dtos;

namespace Application.Common.Interfaces;

public interface ITelegramService
{
    Task SendMessageAsync(IEnumerable<ResponseEconomiaDto> dto);
}
