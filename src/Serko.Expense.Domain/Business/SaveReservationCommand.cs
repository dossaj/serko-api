using Serko.Expense.Domain.Models;

namespace Serko.Expense.Domain.Business;

public class SaveReservationCommand
{
    public Reservation Reservation { get; set; }
}