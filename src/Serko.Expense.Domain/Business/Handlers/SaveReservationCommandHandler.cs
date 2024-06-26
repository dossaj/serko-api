﻿using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Serko.Expense.Core.Cqrs;

namespace Serko.Expense.Domain.Business.Handlers;

public class SaveReservationCommandHandler : IHandleCommand<SaveReservationCommand, int>
{
    private readonly ExpenseContext ctx;

    public SaveReservationCommandHandler(ExpenseContext ctx)
    {
        this.ctx = ctx;
    }

    public async Task<int> Execute(SaveReservationCommand arguments)
    {
        var vendor = await ctx
            .Vendors
            .SingleOrDefaultAsync(x => x.Name == arguments.Reservation.Vendor.Name);

        if (vendor != null)
        {
            arguments.Reservation.Vendor = vendor;
        }

        ctx.Reservations.Add(arguments.Reservation);
        await ctx.SaveChangesAsync();
        return arguments.Reservation.Id;
    }
}