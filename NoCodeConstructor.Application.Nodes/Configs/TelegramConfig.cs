using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NoCodeConstructor.Domain.Configs;

public class TelegramConfig
{
    [JsonRequired]
    [JsonPropertyName("token")]
    public string BotToken { get; set; }
    
    [JsonRequired]
    [JsonPropertyName("chat_id")]
    public int ChatId { get; set; }
    
    [JsonRequired]
    [JsonPropertyName("content")]
    public string Content { get; set; }
}