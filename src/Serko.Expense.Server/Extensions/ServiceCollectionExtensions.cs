using System;
using Microsoft.Extensions.DependencyInjection;

namespace Serko.Expense.Server.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceProvider AddCastle(this IServiceCollection services, Application application)
        {
            return application.Initialize(services);
        }
    }
}