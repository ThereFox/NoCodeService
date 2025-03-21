using CSharpFunctionalExtensions;

namespace NoCodeConstructor.Domain.Scheme.Realisations.Scheme;

public class Pipe : ValueObject
{
    public readonly List<int> Inputs = new List<int>();
    public List<int> Outputs = new List<int>();

    public Pipe(List<int> inputs, List<int> outputs)
    {
        Inputs = inputs;
        Outputs = outputs;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Inputs;
        yield return Outputs;
    }
}