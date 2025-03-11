using CSharpFunctionalExtensions;
using NoCodeConstructor.Domain.Abstactions;
using NoCodeConstructor.Domain.Configs;
using NodeBuilder.Attributes;

namespace NoCodeConstructor.Domain.Actions;

[ActionCode(1)]
public class NothingAction : INodeAction
{
    public NothingAction(TestConfig config)
    {
        Console.WriteLine(config.Value);
    }

    public async Task<Result> Handle(IExecutionContext context)
    {
        await Task.CompletedTask;

        return Result.Success();
    }
}