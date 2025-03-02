using NoCodeConstructor.Domain.Nodes;

namespace NoCodeConstructor.Domain.Scheme.Realisations.Pipes;

public class EndPipe : IPipe
{
    public EndPipe()
    {
        
    }
    
    public Task HandleNextStepAsync(ExecutionContext context)
    {
        return Task.CompletedTask;
    }
}