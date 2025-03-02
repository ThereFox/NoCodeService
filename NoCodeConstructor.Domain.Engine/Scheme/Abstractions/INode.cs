namespace NoCodeConstructor.Domain.Nodes;

public abstract class INode
{
    public int Id { get; }
    public int TypeId { get; }
    public string Name { get; }

    public INode(int Id, int TypeId, string Name)
    {
        Id = Id;
        TypeId = TypeId;
        Name = Name;
    }
}