using CSharpFunctionalExtensions;
using NoCode.Application.Interfaces;
using NodeBuilder.DTOs;

namespace NoCode.Application.UseCases;

public class SaveScheme
{
    private readonly ISchemeActivator _schemeActivator;
    private readonly ISchemeStore _schemeStore;

    public SaveScheme(ISchemeActivator schemeActivator, ISchemeStore schemeStore)
    {
        _schemeActivator = schemeActivator;
        _schemeStore = schemeStore;
    }

    public async Task<Result> SaveWithValidation(Guid id, List<NodeConfigInputObject> nodes)
    {
        var schemeValidate = _schemeActivator.Activate(nodes);

        if (schemeValidate.IsFailure)
        {
            return Result.Failure(schemeValidate.Error);
        }
        
        return await _schemeStore.SaveNewRaw(id, nodes);
    }
}