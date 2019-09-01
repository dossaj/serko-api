using System;
using Castle.MicroKernel;
using FluentValidation;
using Serko.Expense.Core.Cqrs;

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
            return kernel.Resolve<IValidator<T>>();
        }

        public IValidator GetValidator(Type type)
        {
            var t = typeof(IValidator<>)
                .MakeGenericType(type);
            return (IValidator)kernel.Resolve(t);
        }
    }
}