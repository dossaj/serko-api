using System;

namespace Serko.Expense.Core.Factories
{
    public class ServiceProviderFactory : IServiceProviderFactory
    {
        private IServiceProvider instance;

        public object GetService(Type serviceType)
        {
            return instance.GetService(serviceType);
        }

        public IServiceProvider Resolve()
        {
            return instance;
        }

        public void Release(IServiceProvider provider)
        {
        }

        public void Visit(IServiceProvider serviceProvider)
        {
            instance = serviceProvider;
        }
    }
}