using NoCodeConstructor.Domain.Engine;
using NoCodeConstructor.Domain.Nodes;
using ExecutionContext = System.Threading.ExecutionContext;

namespace NoCodeConstructor.Domain.Scheme.Realisations.Pipes;

public class IfPipe : IPipe
{
    public Predicate<ExecutionContext> Predicate { get; init; }
    
    public IActionNode TruePathNode { get; init; }
    public IActionNode FalsePathNode { get; init; }


    public async Task HandleNextStepAsync(ExecutionContext context)
    {
        if (Predicate.Invoke(context))
        {
            await TruePathNode.HandleAsync(context);
        }
        else
        {
            await FalsePathNode.HandleAsync(context);
        }
    }
}