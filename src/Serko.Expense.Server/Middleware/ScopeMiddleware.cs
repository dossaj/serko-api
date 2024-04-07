using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Serko.Expense.Core;

namespace Serko.Expense.Server.Middleware;

public class ScopeMiddleware : IMiddleware
{
    private readonly IScopeManager manager;
    private readonly IServiceProvider provider;

    public ScopeMiddleware(IScopeManager manager, IServiceProvider provider)
    {
        this.manager = manager;
        this.provider = provider;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        using var providerScope = provider.CreateScope();
        using var managerScope = manager.BeginScope();
        
        await next(context);
    }
}
