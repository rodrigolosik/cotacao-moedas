using Refit;

namespace Infra.Gateways;

public interface ITelegramGateway
{
    [Get("/sendMessage?chat_id={chat_id}&text={message}&parse_mode=markdown")]
    Task SendMessageAsync(string chat_id, string message);
}
