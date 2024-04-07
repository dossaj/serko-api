using System.Linq;
using Castle.Core;
using Castle.DynamicProxy;
using Castle.MicroKernel;
using Castle.MicroKernel.ComponentActivator;
using Castle.MicroKernel.LifecycleConcerns;

namespace Serko.Expense.Castle.Facilities;

public class AspNetCoreComponentActivator : DefaultComponentActivator
{
    public AspNetCoreComponentActivator(
        ComponentModel model,
        IKernelInternal kernel,
        ComponentInstanceDelegate onCreation,
        ComponentInstanceDelegate onDestruction) : base(model, kernel, onCreation, onDestruction)
    {
    }

    protected override void ApplyDecommissionConcerns(object instance)
    {
        if (!Model.Lifecycle.HasDecommissionConcerns)
        {
            return;
        }

        instance = ProxyUtil.GetUnproxiedInstance(instance);
        var filtered = Model
            .Lifecycle
            .DecommissionConcerns
            .Where(x => !(x is DisposalConcern));
        ApplyConcerns(filtered, instance);
    }
}