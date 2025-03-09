using System.Text.Json.Serialization;

namespace NoCodeConstructor.Domain.Configs;

public class TestConfig
{
    [JsonPropertyName("test")]
    public string Value { get; set; }
};