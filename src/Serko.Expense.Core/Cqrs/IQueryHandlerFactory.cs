namespace Serko.Expense.Core.Cqrs;

public interface IQueryHandlerFactory
{
    IHandleQuery<TArguments, TResult> Resolve<TArguments, TResult>();
    void Release<TArguments, TResult>(IHandleQuery<TArguments, TResult> handler);
}