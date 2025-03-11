using CSharpFunctionalExtensions;
using NoCodeConstructor.Domain.Abstactions;
using NoCodeConstructor.Domain.Configs;
using NoCodeConstructor.Domain.Scheme.Realisations.Scheme;
using NodeBuilder.Attributes;

namespace NoCodeConstructor.Domain.Triggers;

[ActionCode(2)]
public class AllwaysTrigger : IInputTrigger
{
    public AllwaysTrigger(TestConfig config)
    {
    }

    public Result<bool> IsTriggered(EventInfo eventInfo, IExecutionContext context)
    {
        return Result.Success(true);
    }
}