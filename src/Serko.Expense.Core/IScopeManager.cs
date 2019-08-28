using System;

namespace Serko.Expense.Core
{
    public interface IScopeManager
    {
        IDisposable BeginScope();
    }
}
