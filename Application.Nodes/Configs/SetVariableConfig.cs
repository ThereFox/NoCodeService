using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NoCodeConstructor.Domain.Configs;

public class SetVariableConfig
{
    [Required]
    [JsonPropertyName("name")]
    public string VariableName { get; set; }
    
    [Required]
    [JsonPropertyName("value")]
    public string VariableValue { get; set; }
}