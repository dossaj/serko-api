using Castle.MicroKernel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace Serko.Expense.Castle.Factories;

public class WindsorControllerActivator : IControllerActivator
{
    private readonly IKernel kernel;

    public WindsorControllerActivator(IKernel kernel)
    {
        this.kernel = kernel;
    }

    public object Create(ControllerContext context)
    {
        return kernel.Resolve(context.ActionDescriptor.ControllerTypeInfo.AsType());
    }

    public void Release(ControllerContext context, object controller)
    {
        kernel.ReleaseComponent(controller);
    }
}