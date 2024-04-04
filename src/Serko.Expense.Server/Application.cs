using System;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Castle.Windsor.Proxy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serko.Expense.Castle.Facilities;
using Serko.Expense.Castle.Resolvers;
using Serko.Expense.Core;
using Serko.Expense.Core.Adapters;
using IConfigurationManager = Serko.Expense.Core.IConfigurationManager;

namespace Serko.Expense.Server
{
    public class Application : IDisposable
    {
        private bool disposed;

        public DefaultKernel Kernel { get; protected set; }
        public WindsorContainer Container { get; protected set; }
        public IConfigurationManager Manager { get; protected set; }
        public IServiceCollection Services { get; protected set; }

        public Application(IConfiguration configuration)
        {
            Manager = new ConfigurationManagerAdapter(configuration);
            Kernel = new DefaultKernel(new DefaultDependencyResolver(), new DefaultProxyFactory());
            Container = new WindsorContainer(Kernel, new DefaultComponentInstaller());
        }

        public IServiceProvider Initialize(IServiceCollection services)
        {
            Services = services;
            Initialize();
            return Kernel
                .Resolve<IServiceProviderFactory>()
                .Resolve();
        }

        protected void Initialize()
        {
            using (Kernel.OptimizeDependencyResolution())
            {
                InitializeResolvers();
                InitializeFacilities();
                InitializeComponents();
            }
        }

        protected virtual void InitializeFacilities()
        {
            Container.AddFacility(new TypedFactoryFacility());
            Container.AddFacility(new AspNetCoreFacility(Services));
        }

        protected virtual void InitializeComponents()
        {
            var filter = new AssemblyFilter(AppDomain.CurrentDomain.BaseDirectory, "Serko.*");
            Container.Install(FromAssembly.InDirectory(filter));
        }

        protected virtual void InitializeResolvers()
        {
            Kernel.Resolver.AddSubResolver(new WindsorConfigurationConvention(Manager));
            Kernel.Resolver.AddSubResolver(new CollectionResolver(Container.Kernel, true));
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }

            if (disposing)
            {
                Kernel?.Dispose();
                Container?.Dispose();
            }

            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}