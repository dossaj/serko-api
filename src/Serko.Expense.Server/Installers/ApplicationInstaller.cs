using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Serko.Expense.Castle;
using Serko.Expense.Core;

namespace Serko.Expense.Server.Installers
{
    public class ApplicationInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IScopeManager>()
                    .ImplementedBy<WindsorScopeManager>()
                    .LifestyleSingleton()
            );
        }
    }
}
