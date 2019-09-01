namespace Serko.Expense.Core.Cqrs
{
    public interface ICommandHandlerFactory
    {
        IHandleCommand<TArguments> Resolve<TArguments>();
        void Release<TArguments>(IHandleCommand<TArguments> handler);
    }
}