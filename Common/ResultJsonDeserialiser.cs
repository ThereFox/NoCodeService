using System.Text.Json;
using System.Text.Json.Serialization;
using CSharpFunctionalExtensions;

namespace Common;

public static class ResultJsonDeserialiser
{
    public static Result<T> Deserialise<T>(string json)
    {
        try
        {
            var result = JsonSerializer.Deserialize<T>(json);

            if (result == null)
            {
                return Result.Failure<T>("Could not deserialise");
            }

            return result;
        }
        catch (Exception e)
        {
            return Result.Failure<T>(e.Message);
        }
    }

    public static Result<object> Deserialise(Type type, string json)
    {
        try
        {
            var result = JsonSerializer.Deserialize(
                json,
                type,
                new JsonSerializerOptions()
                {
                    IncludeFields = true,
                    DefaultIgnoreCondition = JsonIgnoreCondition.Never
                }
            );

            if (result == null)
            {
                return Result.Failure<object>("Could not deserialise");
            }

            return result;
        }
        catch (Exception e)
        {
            return Result.Failure<object>(e.Message);
        }
    }
}