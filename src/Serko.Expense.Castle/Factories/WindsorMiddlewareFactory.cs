using System;
using Castle.MicroKernel;
using Microsoft.AspNetCore.Http;

namespace Serko.Expense.Castle.Factories
{
    public class WindsorMiddlewareFactory : IMiddlewareFactory
    {
        private readonly IKernel kernel;

        public WindsorMiddlewareFactory(IKernel kernel)
        {
            this.kernel = kernel;
        }

        public IMiddleware Create(Type middlewareType)
        {
            return (IMiddleware)kernel.Resolve(middlewareType);
        }

        public void Release(IMiddleware middleware)
        {
            kernel.ReleaseComponent(middleware);
        }
    }
}