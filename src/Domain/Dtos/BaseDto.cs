using System.Text.Json.Serialization;

namespace Domain.Dtos;

public record BaseDto
{
    [JsonPropertyName("high")]
    public string High { get; set; }

    [JsonPropertyName("low")]
    public string Low { get; set; }

    [JsonPropertyName("varBid")]
    public string VarBid { get; set; }

    [JsonPropertyName("pctChange")]
    public string PctChange { get; set; }

    [JsonPropertyName("bid")]
    public string Bid { get; set; }

    [JsonPropertyName("ask")]
    public string Ask { get; set; }

    [JsonPropertyName("timestamp")]
    public string Timestamp { get; set; }
}
