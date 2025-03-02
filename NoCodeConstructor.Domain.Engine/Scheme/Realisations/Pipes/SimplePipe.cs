using NoCodeConstructor.Domain.Nodes;

namespace NoCodeConstructor.Domain.Scheme.Realisations.Pipes;

public class SimplePipe : IPipe
{
    public IActionNode NextElement { get; init; }


    public async Task HandleNextStepAsync(ExecutionContext context)
    {
        await NextElement.HandleAsync(context);
    }
}