using NoCodeConstructor.Domain.Nodes;

namespace NoCodeConstructor.Domain.Scheme.Realisations.Nodes;

public class NoActionNode : IActionNode
{
    public int Id { get; init; }
    public int TypeId { get; }
    public string Name { get; }
    public IPipe NextElementPipe { get; }
    public async Task HandleAsync(ExecutionContext context)
    {
        await NextElementPipe.HandleNextStepAsync(context);
    }
}