using System;
using System.Linq;
using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.Context;
using Microsoft.Extensions.DependencyInjection;
using Serko.Expense.Castle.Extensions;
using Serko.Expense.Core;

namespace Serko.Expense.Castle.Resolvers;

public class FrameworkDependencyResolver : ISubDependencyResolver, IServiceProviderVisitor
{
    private IServiceProvider provider;
    private readonly IServiceCollection services;

    public FrameworkDependencyResolver(IServiceCollection services)
    {
        this.services = services;
    }

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
        return dependency.TargetType != null && services.Any(x => x.ServiceType.Matches(dependency.TargetType));
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
        return provider.GetService(dependency.TargetType);
    }
}