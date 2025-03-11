using CSharpFunctionalExtensions;
using NoCodeConstructor.Domain.Abstactions;

namespace NoCodeConstructor.Domain.Scheme.Realisations.Scheme.Entitys;

public class InputNode : Entity<int>
{
    private readonly IInputTrigger _trigger;
    public Pipe OutputPipe { get; }

    private InputNode(int id, Pipe outpute, IInputTrigger trigger) : base(id)
    {
        Id = id;
        _trigger = trigger;
        OutputPipe = outpute;
    }

    public Result<bool> IsTriggering(EventInfo eventInfo, IExecutionContext executionContext)
    {
        return _trigger.IsTriggered(eventInfo, executionContext);
    }

    public static Result<InputNode> Create(int id, IInputTrigger trigger, Pipe outpipe)
    {
        return Result.Success(new InputNode(id, outpipe, trigger));
    }
}