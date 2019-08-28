using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.Context;
using Serko.Expense.Core;

namespace Serko.Expense.Castle.Resolvers
{
    public class WindsorConfigurationConvention : ISubDependencyResolver
    {
        private readonly IConfigurationManager manager;

        public WindsorConfigurationConvention(IConfigurationManager manager)
        {
            this.manager = manager;
        }

        public bool CanResolve(
            CreationContext context,
            ISubDependencyResolver contextHandlerResolver,
            ComponentModel model,
            DependencyModel dependency)
        {
            return dependency.TargetType != null && manager.Has(dependency.TargetType);
        }

        public object Resolve(
            CreationContext context,
            ISubDependencyResolver contextHandlerResolver,
            ComponentModel model,
            DependencyModel dependency)
        {
            return manager.Get(dependency.TargetType);
        }
    }
}