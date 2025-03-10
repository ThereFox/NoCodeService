using MongoDB.Bson.Serialization.Attributes;
using NodeBuilder.DTOs;

namespace NoCodeConstructor.Persistense.DTOs;

public class SchemeDTO
{
    [BsonId] public string Id { get; set; }
    public List<NodeConfigInputObject> Nodes { get; set; }
}