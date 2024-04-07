using System;
using Microsoft.Extensions.Configuration;

namespace Serko.Expense.Server.Extensions;

public static class ConfigurationExtensions
{
    public static object GetSection(this IConfiguration configuration, Type t)
    {
        //the constructor for a configuration model should never have arguments
        //this will then not be slow, however if there is some strange need for this
        //a compiled construction would be faster
        var obj = Activator.CreateInstance(t);
        configuration
            .GetSection(t.Name)
            .Bind(obj);
        return obj;
    }

    public static TResult GetSection<TResult>(this IConfiguration configuration, string key)
        where TResult : new()
    {
        var obj = new TResult();
        configuration
            .GetSection(key)
            .Bind(obj);
        return obj;
    }

    public static TResult GetSection<TResult>(this IConfiguration configuration)
        where TResult : new()
    {
        var obj = new TResult();
        configuration
            .GetSection(typeof(TResult).Name)
            .Bind(obj);
        return obj;
    }
}
