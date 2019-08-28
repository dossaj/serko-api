using System;
using Castle.MicroKernel.Lifestyle.Scoped;
using Serko.Expense.Core;

namespace Serko.Expense.Castle
{
    public class WindsorScopeManager : IScopeManager
    {
        public IDisposable BeginScope()
        {
            return new CallContextLifetimeScope();
        }
    }
}