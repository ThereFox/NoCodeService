using CSharpFunctionalExtensions;
using NoCode.Application.Interfaces;
using NoCodeConstructor.Domain.Scheme.Realisations.Scheme;

namespace NoCode.Application.UseCases;

public class ExecuteSavedScheme
{
    private readonly ISchemeStore _store;

    public ExecuteSavedScheme(ISchemeStore store)
    {
        _store = store;
    }

    public async Task<Result> RunById(Guid id)
    {
        var getSchemeResult = await _store.GetById(id);

        if (getSchemeResult.IsFailure)
        {
            return getSchemeResult.ConvertFailure();
        }

        var handleResult = await getSchemeResult.Value.HandleEvent(new EventInfo());

        return handleResult;
    }
}