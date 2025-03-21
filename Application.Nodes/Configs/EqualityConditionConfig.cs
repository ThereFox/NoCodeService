using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NoCodeConstructor.Domain.Configs;

public class EqualityConditionConfig
{
    [Required]
    [JsonPropertyName("checked")]
    public string Value { get; set; }
    [Required]
    [JsonPropertyName("expected")]
    public string Expected { get; set; }
    
    [JsonPropertyName("trueNode")]
    public int TrueNodeId { get; set; }
    [JsonPropertyName("falseNode")]
    public int FalseNodeId { get; set; }
}