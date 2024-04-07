using System;

namespace Serko.Expense.Castle.Extensions;

public static class TypeExtensions
{
    public static bool Matches(this Type source, Type value)
    {
        var type = source.IsGenericType ? source.GetGenericTypeDefinition() : source;
        var valueType = value.IsGenericType ? value.GetGenericTypeDefinition() : value;
        return type == valueType || valueType == type;
    }
}