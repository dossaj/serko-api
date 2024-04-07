using System;

namespace Serko.Expense.Core;

public interface IConfigurationManager
{
    bool Has(Type type);
    bool Has<T>() where T : class;
    object Get(Type type);
    T Get<T>() where T : class;
}