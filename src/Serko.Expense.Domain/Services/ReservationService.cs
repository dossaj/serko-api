using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Serko.Expense.Core.Cqrs;
using Serko.Expense.Domain.Business;
using Serko.Expense.Domain.Models;

namespace Serko.Expense.Domain.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IExecute execute;

        public ReservationService(IExecute execute)
        {
            this.execute = execute;
        }

        public async Task<Reservation> Get(int id)
        {
            return (await execute
                .Query<ReservationQuery, List<Reservation>>(new ReservationQuery { Id = id }))
                .SingleOrDefault();
        }

        public Task<List<Reservation>> Get()
        {
            return execute.Query<ReservationQuery, List<Reservation>>(new ReservationQuery());
        }

        public Task<int> Save(Reservation reservation)
        {
            return execute.Command<SaveReservationCommand, int>(new SaveReservationCommand { Reservation = reservation });
        }
    }
}
