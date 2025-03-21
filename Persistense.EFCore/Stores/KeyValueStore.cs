using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using NoCodeConstructor.Nodes.Interfaces;
using Persistense.EFCore.DTOs;
using Persistense.EFCore.DTOs.DTOs;

namespace Persistense.EFCore.Stores;

public class KeyValueStore : IKeyValueStore
{
    private readonly ApplicationContext _context;

    public KeyValueStore(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<Result<string>> GetValue(string key)
    {
        try
        {
            var value = await _context.KeyValueRecords
                .AsNoTracking()
                .Where(ex => ex.Key == key)
                .FirstOrDefaultAsync();

            if (value == default)
            {
                return Result.Failure<string>("Key not found");
            }
            return Result.Success(value.Value);
        }
        catch (Exception ex)
        {
            return Result.Failure<string>(ex.Message);
        }
    }

    public async Task<Result> SetValue(string key, string value)
    {
        try
        {
            var record = new KeyValueRecord(key, Guid.Empty, value);

            await _context.KeyValueRecords.AddAsync(record);

            await _context.SaveChangesAsync();

            return Result.Success();
        }
        catch (EntityFramework.Exceptions.Common.UniqueConstraintException ex)
        {
            return await SetValue(key, value);
        }
        catch (Exception e)
        {
            return Result.Failure(e.Message);
        }
    }
}