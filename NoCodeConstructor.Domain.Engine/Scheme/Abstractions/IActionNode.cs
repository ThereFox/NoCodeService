using NoCodeConstructor.Domain.Scheme.Realisations.Pipes;

namespace NoCodeConstructor.Domain.Nodes;

public abstract class IActionNode : INode
{
    public IPipe NextElementPipe { get; }
    
    public abstract Task HandleAsync(ExecutionContext context);

    public IActionNode(int id, int typeId, string Name,  IPipe nextPipe) : base(id, typeId, Name)
    {
        NextElementPipe = nextPipe;
    }
    public IActionNode(int id, int typeId, string Name) : base(id, typeId, Name)
    {
        NextElementPipe = new EndPipe();
    }
}