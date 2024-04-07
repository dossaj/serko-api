using System;

namespace Serko.Expense.Domain.Models;

public class Reservation
{
    public int Id { get; set; }
    public Vendor Vendor { get; set; }
    public Expense Expense { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; }
}