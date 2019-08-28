using System;

namespace Serko.Expense.Core
{
    public class NullScopeManager : IScopeManager
    {
        public IDisposable BeginScope()
        {
            return new NullDisposable();
        }

        private class NullDisposable : IDisposable
        {
            public void Dispose()
            {
            }
        }
    }
}