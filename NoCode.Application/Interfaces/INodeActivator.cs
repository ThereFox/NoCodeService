using CSharpFunctionalExtensions;
using NoCodeConstructor.Domain.Scheme.Realisations.Scheme;
using NoCodeConstructor.Domain.Scheme.Realisations.Scheme.Entitys;
using NodeBuilder.DTOs;

namespace NoCode.Application.Interfaces;

public interface ISchemeActivator
{
    public Result<CodeScheme> Activate(List<NodeConfigInputObject> nodes);
}