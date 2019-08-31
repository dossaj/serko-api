namespace Serko.Expense.Server.Dtos
{
    public class ExpenseDto
    {
        public string CostCentre { get; set; }
        public decimal Total { get; set; }
        public string PaymentMethod { get; set; }
    }
}