using System.Reflection;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using FluentValidation;

namespace Serko.Expense.Server.Installers
{
    public class ValidatorsInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Classes
                    .FromAssemblyInThisApplication(Assembly.GetExecutingAssembly())
                    .BasedOn(typeof(IValidator<>))
                    .WithServiceBase()
                    .LifestyleTransient()
            );
        }
    }
}