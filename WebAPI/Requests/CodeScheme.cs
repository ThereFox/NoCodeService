using System.Text.Json.Serialization;

namespace WebAPI.Requests;

public class CodeScheme
{
    [JsonRequired]
    public List<CodeNode> Nodes { get; init; }
}