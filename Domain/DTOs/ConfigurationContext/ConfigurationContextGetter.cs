using System.Net.Mime;
using NoCodeConstructor.Domain.Abstactions;

namespace NoCodeConstructor.Domain.DTOs.ConfigurationContext;

public class ConfigurationGetter
{
    private IVariableContext _context;

    public ConfigurationGetter(IVariableContext context)
    {
        _context = context;
    }

    public ConfigurationType GetConfiguration<ConfigurationType>(ConfigurationType initConfig)
    {
        var configuration = Activator.CreateInstance<ConfigurationType>();

        var properties = initConfig.GetType().GetProperties();
        
        foreach (var property in properties)
        {
            var initData = property.GetValue(initConfig);

            if (initData is not string || initData is null)
            {
                property.SetValue(configuration, initData);
                continue;
            }
            
            var stringValue = initData as string;
            
            var settableValue = stringValue.StartsWith("${") ?
                _context.GetValue(stringValue)
                : stringValue;
            
            property.SetValue(configuration, settableValue);
        }

        return configuration;
    }
    
}