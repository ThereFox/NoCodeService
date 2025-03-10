using CSharpFunctionalExtensions;
using NoCodeConstructor.Domain.Scheme.Realisations.Scheme;
using NodeBuilder.DTOs;

namespace NoCode.Application.Interfaces;

public interface ISchemeStore
{
    public Task<Result<CodeScheme>> GetById(Guid id);
    public Task<Result> SaveNew(Guid id, CodeScheme scheme);
    public Task<Result> SaveNewRaw(Guid id, List<NodeConfigInputObject> scheme);
}