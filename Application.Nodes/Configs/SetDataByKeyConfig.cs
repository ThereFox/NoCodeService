using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NoCodeConstructor.Domain.Configs;

public class SetDataByKeyConfig
{
    [Required]
    [JsonPropertyName("key")]
    public string Key { get; set; }
    
    [Required]
    [JsonPropertyName("returnName")]
    public string Value { get; set; }
}