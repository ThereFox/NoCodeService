using NoCodeConstructor.Domain.Nodes;

namespace NoCodeConstructor.Domain.Scheme.Realisations.Nodes;

public class NoActionNode : IActionNode
{
    public IPipe NextElementPipe { get; }
    public override async Task HandleAsync(ExecutionContext context)
    {
        await NextElementPipe.HandleNextStepAsync(context);
    }
}