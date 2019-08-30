namespace Serko.Expense.Domain
{
    public class Expense
    {
        public string CostCentre { get; set; }
        public decimal Total { get; set; }
        public string PaymentMethod { get; set; }
    }
}