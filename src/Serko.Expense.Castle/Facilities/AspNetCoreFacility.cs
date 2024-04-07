using System;
using System.Collections.Generic;
using Castle.Core.Internal;
using Castle.MicroKernel;
using Castle.MicroKernel.Context;
using Castle.MicroKernel.Facilities;
using Castle.MicroKernel.ModelBuilder;
using Castle.MicroKernel.Registration;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Serko.Expense.Castle.Factories;
using Serko.Expense.Castle.Resolvers;
using Serko.Expense.Core;
using Serko.Expense.Core.Factories;

namespace Serko.Expense.Castle.Facilities;

public class AspNetCoreFacility : AbstractFacility
{
    private HashSet<IHandler> handlers;
    private readonly IServiceCollection services;
    private readonly IContributeComponentModelConstruction contributor;

    public AspNetCoreFacility(IServiceCollection services)
    {
        this.services = services;
        this.contributor = new AspNetCoreComponentModelContributor(services);
    }

    protected override void Init()
    {
        handlers = new HashSet<IHandler>();
        Kernel.ComponentModelBuilder.AddContributor(contributor);
        Kernel.ComponentRegistered += (key, handler) =>
        {
            if (handler.ComponentModel.Implementation.Is<IServiceProviderVisitor>())
            {
                handlers.Add(handler);
            }
        };
        Kernel.RegistrationCompleted += (sender, args) =>
        {
            var provider = services.BuildServiceProvider(false);
            foreach (var handler in handlers)
            {
                var h = Resolve<IServiceProviderVisitor>(handler);
                h.Visit(provider);
            }
        };

        RegisterCastle();
        RegisterAspNetCore();
    }

    protected virtual void RegisterCastle()
    {
        var logger = new LoggerDependencyResolver();
        var framework = new FrameworkDependencyResolver(services);

        Kernel.Register(
            Component.For<IServiceProviderVisitor>()
                .Instance(logger)
                .LifestyleSingleton(),
            Component.For<IServiceProviderVisitor>()
                .Instance(framework)
                .LifestyleSingleton(),
            Component.For<IServiceProvider>()
                .Forward<IServiceProviderFactory>()
                .Forward<IServiceProviderVisitor>()
                .ImplementedBy<ServiceProviderFactory>()
                .LifestyleSingleton()
        );
        Kernel.Resolver.AddSubResolver(logger);
        Kernel.Resolver.AddSubResolver(framework);
    }

    protected virtual void RegisterAspNetCore()
    {
        //should add in other registrations for aspnet core but not needed for an API
        services.AddSingleton<IControllerActivator>(new WindsorControllerActivator(Kernel));
        services.AddSingleton<IMiddlewareFactory>(new WindsorMiddlewareFactory(Kernel));
        services.AddSingleton<IValidatorFactory>(new WindsorValidatorFactory(Kernel));
    }

    protected virtual TType Resolve<TType>(IHandler handler)
    {
        var ctx = new CreationContext(handler, Kernel.ReleasePolicy, handler.ComponentModel.Implementation, null, null, null);
        return (TType)handler.Resolve(ctx);
    }
}