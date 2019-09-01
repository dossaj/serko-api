using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Serko.Expense.Core.Cqrs;
using Serko.Expense.Domain.Models;

namespace Serko.Expense.Domain.Business.Handlers
{
    public class GetReservationQueryHandler : IHandleQuery<ReservationQuery, List<Reservation>>
    {
        private readonly ExpenseContext ctx;

        public GetReservationQueryHandler(ExpenseContext ctx)
        {
            this.ctx = ctx;
        }

        public Task<List<Reservation>> Execute(ReservationQuery query)
        {
            return ctx.Reservations
                .Include(x => x.Vendor)
                .Include(x => x.Expense)
                .Where(x => query.Id == null || x.Id == query.Id)
                .ToListAsync();
        }
    }
}