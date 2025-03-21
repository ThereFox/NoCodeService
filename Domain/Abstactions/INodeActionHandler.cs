using CSharpFunctionalExtensions;
using ExecutionContext = NoCodeConstructor.Domain.DTOs.ExecutionContext;

namespace NoCodeConstructor.Domain.Abstactions;

public interface INodeAction
{
    public Task<Result> Handle(ExecutionContext context);
}