namespace NoCodeConstructor.Domain.Nodes;

public abstract class IInputNode : INode
{
    public IPipe NextElementPipe { get; }
    
    protected IInputNode(int Id, int TypeId, string Name, IPipe nextElementPipe) : base(Id, TypeId, Name)
    {
        NextElementPipe = nextElementPipe;
    }
}