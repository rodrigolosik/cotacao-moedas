namespace Domain.Configurations;

public record TelegramConfiguration
{
    public string ApiToken { get; set; }
    public string ChatId { get; set; }
}
