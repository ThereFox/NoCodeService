using NoCodeConstructor.Domain.Abstactions;
using NoCodeConstructor.Domain.DTOs.ConfigurationContext;
using NoCodeConstructor.Domain.Scheme.Realisations.Scheme;

namespace NoCodeConstructor.Domain.DTOs;

public class ExecutionContext
{
    public ConfigurationGetter Configuration { get; }
    public Pipe OutputePipe { get; }
    public IVariableContext VariableContext { get; }

    public ExecutionContext(IVariableContext context, Pipe outputePipe)
    {
        Configuration = new ConfigurationGetter(context);
        OutputePipe = outputePipe;
        VariableContext = context;
    }
}