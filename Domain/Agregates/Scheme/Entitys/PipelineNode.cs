using CSharpFunctionalExtensions;
using NoCodeConstructor.Domain.Abstactions;
using NoCodeConstructor.Domain.DTOs.ConfigurationContext;
using ExecutionContext = NoCodeConstructor.Domain.DTOs.ExecutionContext;

namespace NoCodeConstructor.Domain.Scheme.Realisations.Scheme.Entitys;

public class PipelineNode : Entity<int>
{
    
    private readonly INodeAction _action;

    public readonly Pipe OutputPipe;

    private bool IsAlreadyExecuted = false;

    public async Task<Result> Execute(IVariableContext context)
    {
        if (IsAlreadyExecuted)
        {
            return Result.Failure("task already executed");
        }

        var executionContext = new ExecutionContext(context, OutputPipe);
        
        return await _action.Handle(executionContext);
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