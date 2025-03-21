using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NoCodeConstructor.Domain.Configs;

public class SendTelegramMessageConfig
{
    [JsonRequired]
    [JsonPropertyName("token")]
    public string BotToken { get; set; }

    [JsonRequired]
    [JsonPropertyName("chat_id")]
    public string ChatId { get; set; }

    [JsonRequired]
    [JsonPropertyName("content")]
    public string Content { get; set; }
}