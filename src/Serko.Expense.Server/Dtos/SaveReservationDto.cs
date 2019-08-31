using System;

namespace Serko.Expense.Server.Dtos
{
    public class SaveReservationDto
    {
        public string Vendor { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public ExpenseDto Expense { get; set; }
    }
}