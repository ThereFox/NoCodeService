using CSharpFunctionalExtensions;

namespace NoCodeConstructor.Domain.Abstactions;

public interface INodeAction
{
    public Task<Result> Handle(ExecutionContext context);
}