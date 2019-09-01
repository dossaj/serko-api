namespace Serko.Expense.Core.Cqrs
{
    public interface ICommandHandlerFactory
    {
        IHandleCommand<TArguments> Resolve<TArguments>();
        IHandleCommand<TArguments, TResult> Resolve<TArguments, TResult>();
        void Release(object handler);
    }
}