using System;

namespace Serko.Expense.Core
{
    public interface IServiceProviderVisitor
    {
        void Visit(IServiceProvider serviceProvider);
    }
}