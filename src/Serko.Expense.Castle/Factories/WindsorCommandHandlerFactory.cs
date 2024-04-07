using Castle.MicroKernel;
using Serko.Expense.Core.Cqrs;

namespace Serko.Expense.Castle.Factories;

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

    public IHandleCommand<TArguments, TResult> Resolve<TArguments, TResult>()
    {
        return kernel.Resolve<IHandleCommand<TArguments, TResult>>();
    }

    public void Release(object handler)
    {
        kernel.ReleaseComponent(handler);
    }
}
