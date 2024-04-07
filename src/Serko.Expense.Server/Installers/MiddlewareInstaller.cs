using System.Reflection;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Microsoft.AspNetCore.Http;

namespace Serko.Expense.Server.Installers;

public class MiddlewareInstaller : IWindsorInstaller
{
    public void Install(IWindsorContainer container, IConfigurationStore store)
    {
        container.Register(
            Classes
                .FromAssemblyInThisApplication(Assembly.GetExecutingAssembly())
                .BasedOn<IMiddleware>()
                .WithServiceSelf()
                .LifestyleSingleton()
        );
    }
}