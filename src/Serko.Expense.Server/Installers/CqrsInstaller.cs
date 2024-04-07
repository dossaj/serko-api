using System.Reflection;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Serko.Expense.Castle.Factories;
using Serko.Expense.Core.Cqrs;

namespace Serko.Expense.Server.Installers;

public class CqrsInstaller : IWindsorInstaller
{
    public void Install(IWindsorContainer container, IConfigurationStore store)
    {
        container.Register(
            Component.For<IExecute>()
                .ImplementedBy<Executor>()
                .LifestyleSingleton(),
            Component.For<ICommandHandlerFactory>()
                .ImplementedBy<WindsorCommandHandlerFactory>()
                .LifestyleSingleton(),
            Component.For<IQueryHandlerFactory>()
                .ImplementedBy<WindsorQueryHandlerFactory>()
                .LifestyleSingleton(),
            Classes
                .FromAssemblyInThisApplication(Assembly.GetExecutingAssembly())
                .BasedOn(typeof(IHandleCommand<>))
                .WithServiceBase()
                .LifestyleScoped(),
            Classes
                .FromAssemblyInThisApplication(Assembly.GetExecutingAssembly())
                .BasedOn(typeof(IHandleCommand<,>))
                .WithServiceBase()
                .LifestyleScoped(),
            Classes
                .FromAssemblyInThisApplication(Assembly.GetExecutingAssembly())
                .BasedOn(typeof(IHandleQuery<,>))
                .WithServiceBase()
                .LifestyleScoped()
        );
    }
}