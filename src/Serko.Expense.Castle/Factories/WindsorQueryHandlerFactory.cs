using Castle.MicroKernel;
using Serko.Expense.Core.Cqrs;

namespace Halter.Fencing.Core.Windsor.Factories
{
    public class WindsorQueryHandlerFactory : IQueryHandlerFactory
    {
        private readonly IKernel kernel;

        public WindsorQueryHandlerFactory(IKernel kernel)
        {
            this.kernel = kernel;
        }

        public IHandleQuery<TArguments, TResult> Resolve<TArguments, TResult>()
        {
            return kernel.Resolve<IHandleQuery<TArguments, TResult>>();
        }

        public void Release<TArguments, TResult>(IHandleQuery<TArguments, TResult> handler)
        {
            kernel.ReleaseComponent(handler);
        }
    }
}