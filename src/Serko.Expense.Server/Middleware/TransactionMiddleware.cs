﻿using System;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Serko.Expense.Core;

namespace Serko.Expense.Server.Middleware
{
    public class TransactionMiddleware : IMiddleware
    {
        private readonly IScopeManager manager;
        private readonly IServiceProvider provider;

        public TransactionMiddleware(IScopeManager manager, IServiceProvider provider)
        {
            this.manager = manager;
            this.provider = provider;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            using (manager.BeginScope())
            using (provider.CreateScope())
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await next(context);
                scope.Complete();
            }
        }
    }
}