using System;
using Castle.Core;
using Castle.Core.Internal;
using Castle.MicroKernel;
using Castle.MicroKernel.ModelBuilder;
using Microsoft.Extensions.DependencyInjection;

namespace Serko.Expense.Castle.Facilities
{
    public class AspNetCoreComponentModelContributor : IContributeComponentModelConstruction
    {
        private readonly IServiceCollection services;

        public AspNetCoreComponentModelContributor(IServiceCollection services)
        {
            this.services = services;
        }

        public void ProcessModel(IKernel kernel, ComponentModel model)
        {
            if (model.CustomComponentActivator.Is<AspNetCoreComponentActivator>())
            {
                foreach (var serviceType in model.Services)
                {
                    switch (model.LifestyleType)
                    {
                        case LifestyleType.Transient:
                            services.AddTransient(serviceType, p => kernel.Resolve(serviceType));
                            break;
                        case LifestyleType.Scoped:
                            services.AddScoped(serviceType, p => kernel.Resolve(serviceType));
                            break;
                        case LifestyleType.Singleton:
                            services.AddSingleton(serviceType, p => kernel.Resolve(serviceType));
                            break;
                        default:
                            throw new NotSupportedException($"Facility only supports the following lifestyles: {nameof(LifestyleType.Transient)}, {nameof(LifestyleType.Scoped)} and {nameof(LifestyleType.Singleton)}.");
                    }
                }
            }
        }
    }
}