using System;

namespace Serko.Expense.Domain
{
    public class Reservation
    {
        public Vendor Vendor { get; set; }
        public Expense Expense { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
    }
}