using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Serko.Expense.Core;

namespace Serko.Expense.Server.Middleware
{
    public class ScopeMiddleware : IMiddleware
    {
        private readonly IScopeManager manager;

        public ScopeMiddleware(IScopeManager manager)
        {
            this.manager = manager;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            using (manager.BeginScope())
            {
                await next(context);
            }
        }
    }
}
