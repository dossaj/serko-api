using System;

namespace Serko.Expense.Core
{
    public interface IServiceProviderFactory : IServiceProvider, IServiceProviderVisitor
    {
        IServiceProvider Resolve();
        void Release(IServiceProvider provider);
    }
}