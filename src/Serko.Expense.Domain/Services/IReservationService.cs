using System.Collections.Generic;
using System.Threading.Tasks;
using Serko.Expense.Domain.Models;

namespace Serko.Expense.Domain.Services
{
    public interface IReservationService
    {
        Task<Reservation> Get(int id);
        Task<List<Reservation>> Get();
        Task Save(Reservation reservation);
    }
}