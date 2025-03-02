namespace NoCodeConstructor.Domain.Nodes;

public interface IPipe
{
    public Task HandleNextStepAsync(ExecutionContext context);
}