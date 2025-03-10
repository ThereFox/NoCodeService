using System.Text.Json.Serialization;

namespace WebAPI.Requests;

public class CodeNode
{
    [JsonPropertyName("id")]
    [JsonRequired]
    public int Id { get; init; }

    [JsonPropertyName("type")]
    [JsonRequired]
    public int TypeId { get; init; }

    [JsonPropertyName("nextElements")] public List<int> ConnectedElements { get; init; }
    [JsonPropertyName("specs")] public object Configuration { get; init; }
}