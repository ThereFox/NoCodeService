using CSharpFunctionalExtensions;
using NoCodeConstructor.Domain.Abstactions;

namespace NoCodeConstructor.Domain.Scheme.Realisations.Scheme.Entitys;

public class PipelineNode : Entity<int>
{
    private readonly INodeAction _action;

    public readonly Pipe OutputPipe;

    private bool IsAlreadyExecuted = false;

    public async Task<Result> Execute(ExecutionContext context)
    {
        if (IsAlreadyExecuted)
        {
            return Result.Failure("task already executed");
        }

        return await _action.Handle(context);
    }

    private PipelineNode(int id, INodeAction action, Pipe outputPipe)
    {
        Id = id;
        _action = action;
        OutputPipe = outputPipe;
    }

    public static Result<PipelineNode> Create(int id, INodeAction action, Pipe outputPipe)
    {
        if (action == null)
        {
            return Result.Failure<PipelineNode>("Action code cannot be null");
        }

        return Result.Success(new PipelineNode(id, action, outputPipe));
    }
}