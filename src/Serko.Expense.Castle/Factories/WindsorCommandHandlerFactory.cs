using Castle.MicroKernel;
using Serko.Expense.Core.Cqrs;

namespace Halter.Fencing.Core.Windsor.Factories
{
    public class WindsorCommandHandlerFactory : ICommandHandlerFactory
    {
        private readonly IKernel kernel;

        public WindsorCommandHandlerFactory(IKernel kernel)
        {
            this.kernel = kernel;
        }

        public IHandleCommand<TArguments> Resolve<TArguments>()
        {
            return kernel.Resolve<IHandleCommand<TArguments>>();
        }

        public void Release<TArguments>(IHandleCommand<TArguments> handler)
        {
            kernel.ReleaseComponent(handler);
        }
    }
}
