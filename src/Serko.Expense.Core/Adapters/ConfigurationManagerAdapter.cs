using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Serko.Expense.Core.Extensions;

namespace Serko.Expense.Core.Adapters;

public class ConfigurationManagerAdapter : IConfigurationManager
{
    private readonly IConfiguration configuration;

    public ConfigurationManagerAdapter(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public bool Has(Type type)
    {
        var result = configuration
            .GetChildren()
            .Any(x => x.Key == type.Name);
        return result;
    }

    public bool Has<T>() where T : class
    {
        return Has(typeof(T));
    }

    public object Get(Type type)
    {
        return configuration.GetSection(type);
    }

    public T Get<T>() where T : class
    {
        return (T)Get(typeof(T));
    }
}
