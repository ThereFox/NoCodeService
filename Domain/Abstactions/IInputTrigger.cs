using CSharpFunctionalExtensions;
using NoCodeConstructor.Domain.Scheme.Realisations.Scheme;

namespace NoCodeConstructor.Domain.Abstactions;

public interface IInputTrigger
{
    public Result<bool> IsTriggered(EventInfo eventInfo, IVariableContext variableContext);
}