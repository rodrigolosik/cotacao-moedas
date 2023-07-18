using System.Text.Json.Serialization;

namespace Domain.Dtos;

public record ResponseEconomiaDto : BaseDto
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    [JsonPropertyName("create_date")]
    public string CreateDate { get; set; }
}
