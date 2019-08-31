using System.Threading.Tasks;
using Serko.Expense.Domain.Models;

namespace Serko.Expense.Domain.Services
{
    public interface IReservationService
    {
        Task Add(Reservation reservation);
    }
}