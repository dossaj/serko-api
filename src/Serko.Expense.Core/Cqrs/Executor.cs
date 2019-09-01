using System.Threading.Tasks;

namespace Serko.Expense.Core.Cqrs
{
    public class Executor : IExecute
    {
        private readonly ICommandHandlerFactory commandFactory;
        private readonly IQueryHandlerFactory queryFactory;

        public Executor(ICommandHandlerFactory commandFactory, IQueryHandlerFactory queryFactory)
        {
            this.commandFactory = commandFactory;
            this.queryFactory = queryFactory;
        }

        public Task Command<TArguments>(TArguments arguments)
        {
            var handler = commandFactory.Resolve<TArguments>();
            try
            {
                return handler.Execute(arguments);
            }
            finally
            {
                commandFactory.Release(handler);
            }
        }

        public Task<TResult> Query<TArguments, TResult>(TArguments arguments)
        {
            var handler = queryFactory.Resolve<TArguments, TResult>();
            try
            {
                return handler.Execute(arguments);
            }
            finally
            {
                queryFactory.Release(handler);
            }
        }
    }
}