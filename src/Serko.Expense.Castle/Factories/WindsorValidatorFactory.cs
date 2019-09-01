using System;
using Castle.MicroKernel;
using FluentValidation;

namespace Serko.Expense.Castle.Factories
{
    public class WindsorValidatorFactory : IValidatorFactory
    {
        private readonly IKernel kernel;

        public WindsorValidatorFactory(IKernel kernel)
        {
            this.kernel = kernel;
        }

        public IValidator<T> GetValidator<T>()
        {
            return kernel.HasComponent(typeof(IValidator<T>)) ? kernel.Resolve<IValidator<T>>() : null;
        }

        public IValidator GetValidator(Type type)
        {
            var t = typeof(IValidator<>)
                .MakeGenericType(type);
            return kernel.HasComponent(t) ? (IValidator)kernel.Resolve(t) : null;
        }
    }
}