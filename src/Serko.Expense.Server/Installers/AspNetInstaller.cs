using System.Reflection;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Microsoft.AspNetCore.Mvc;

namespace Serko.Expense.Server.Installers;

public class AspNetInstaller : IWindsorInstaller
{
    public void Install(IWindsorContainer container, IConfigurationStore store)
    {
        container.Register(
            Classes
                .FromAssemblyInThisApplication(Assembly.GetExecutingAssembly())
                .BasedOn<ControllerBase>()
                .WithServiceSelf()
                .LifestyleScoped()
        );
    }
}