using NoCodeConstructor.Domain.Nodes;

namespace NoCodeConstructor.Domain.Scheme.Realisations.Nodes;

public class LogNode : IActionNode
{
    public LogNode(int id, int typeId, string Name, string name, IPipe nextElementPipe) : base(id, typeId, Name)
    {
        Id = id;
        TypeId = typeId;
        this.Name = name;
        NextElementPipe = nextElementPipe;
    }

    public int Id { get; init; }
    public int TypeId { get; }
    public string Name { get; }
    public IPipe NextElementPipe { get; }
    
    
    public override Task HandleAsync(ExecutionContext context)
    {
        throw new NotImplementedException();
    }
}