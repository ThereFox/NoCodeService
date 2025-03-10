using CSharpFunctionalExtensions;
using MongoDB.Driver;
using NoCode.Application.Interfaces;
using NoCodeConstructor.Domain.Scheme.Realisations.Scheme;
using NoCodeConstructor.Persistense.DTOs;
using NodeBuilder.DTOs;

namespace NoCodeConstructor.Persistense.Stores;

public class SchemeStore : ISchemeStore
{
    private const string defaultCollectionName = "delete_me";
    
    private readonly IMongoDatabase _client;
    private readonly ISchemeActivator _schemeActivator;
    
    public SchemeStore(IMongoDatabase client, ISchemeActivator activator)
    {
        _client = client;
        _schemeActivator = activator;
    }

    public async Task<Result<CodeScheme>> GetById(Guid id)
    {
        var schemeCursor = await _client
            .GetCollection<SchemeDTO>(defaultCollectionName)
            .FindAsync(ex => ex.Id.ToLower().Contains(id.ToString().ToLower()));

        var scheme = (await schemeCursor.ToListAsync()).FirstOrDefault();

        if (scheme == null)
        {
            return Result.Failure<CodeScheme>("Scheme not found");
        }

        var codeActivateResult = _schemeActivator.Activate(scheme.Nodes);

        if (codeActivateResult.IsFailure)
        {
            return Result.Failure<CodeScheme>(codeActivateResult.Error);
        }
        
        return Result.Success(codeActivateResult.Value);
        
    }

    public async Task<Result> SaveNew(Guid id, CodeScheme scheme)
    {
        var nodes = scheme.Nodes
            .Select(ex =>
            {
                return new NodeConfigInputObject(ex.Id, 12, ex.OutputPipe.Outputs, "");
            })
            .ToList();

        var schemeDTO = new SchemeDTO()
        {
            Nodes = nodes,
            Id = id.ToString()
        };

        await _client.GetCollection<SchemeDTO>(defaultCollectionName)
            .InsertOneAsync(schemeDTO);

        return Result.Success();
    }

    public async Task<Result> SaveNewRaw(Guid id, List<NodeConfigInputObject> scheme)
    {
        var schemeDTO = new SchemeDTO()
        {
            Nodes = scheme,
            Id = id.ToString()
        };

        await _client.GetCollection<SchemeDTO>(defaultCollectionName)
            .InsertOneAsync(schemeDTO);

        return Result.Success();
    }
}