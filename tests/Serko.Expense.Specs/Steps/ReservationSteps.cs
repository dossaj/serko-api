using System.Linq;
using TechTalk.SpecFlow;

namespace Serko.Expense.Specs.Steps;

[Binding]
public class ReservationSteps
{
    private readonly ApiContext context;

    public ReservationSteps(ApiContext context)
    {
        this.context = context;
    }

    [Given(@"I have an the following expense:")]
    public void GivenIHaveAnTheFollowingExpense(Table table)
    {
        var dict = table.Rows.ToDictionary(r => r[0], r => (object)r[1]);
        context.Reservation.Add("expense", dict);
    }

    [Given(@"I have an the following reservation:")]
    public void GivenIHaveAnTheFollowingReservation(Table table)
    {
        context.Reservation = table.Rows.ToDictionary(r => r[0], r => (object)r[1]);
    }
}