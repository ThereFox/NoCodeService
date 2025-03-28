using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NoCodeConstructor.Domain.Configs;

public class GetDataByKeyConfig
{
    [Required]
    [JsonPropertyName("key")]
    public string Key { get; set; }
    
    [Required]
    [JsonPropertyName("returnName")]
    public string Name { get; set; }
}