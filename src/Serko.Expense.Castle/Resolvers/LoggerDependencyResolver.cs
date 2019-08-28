using System;
using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.Context;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serko.Expense.Core;

namespace Serko.Expense.Castle.Resolvers
{
    public class LoggerDependencyResolver : ISubDependencyResolver, IServiceProviderVisitor
    {
        private IServiceProvider provider;

        public void Visit(IServiceProvider serviceProvider)
        {
            provider = serviceProvider;
        }

        public bool CanResolve(
            CreationContext context,
            ISubDependencyResolver contextHandlerResolver,
            ComponentModel model,
            DependencyModel dependency)
        {
            return dependency.TargetType == typeof(ILogger);
        }

        public object Resolve(
            CreationContext context,
            ISubDependencyResolver contextHandlerResolver,
            ComponentModel model,
            DependencyModel dependency)
        {
            if (provider == null)
            {
                throw new ArgumentNullException(nameof(provider));
            }
            return provider
                .GetService<ILoggerFactory>()
                .CreateLogger(model.Name);
        }
    }
}