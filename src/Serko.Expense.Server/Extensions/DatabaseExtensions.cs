using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serko.Expense.Domain;
using Serko.Expense.Domain.Models;
using Serko.Expense.Server.Configuration;

namespace Serko.Expense.Server.Extensions;

public static class DatabaseExtensions
{
    public static void InitializeDatabase(this IApplicationBuilder app, Database configuration)
    {
        var scopeFactory = app
            .ApplicationServices
            .GetRequiredService<IServiceScopeFactory>();

        using var scope = scopeFactory.CreateScope();
        var ctx = scope
            .ServiceProvider
            .GetRequiredService<ExpenseContext>();

        if (!ctx.Database.IsInMemory())
        {
            ctx.Database.Migrate();
        }
            
        if (!ctx.Vendors.Any())
        {
            ctx.Vendors.AddRange(
                configuration
                    .Vendors
                    .Select(x => new Vendor{ Name = x })
            );
        }
    }
}