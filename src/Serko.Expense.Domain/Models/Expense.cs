namespace Serko.Expense.Domain.Models;

public class Expense
{
    public int Id { get; set; }
    public decimal Gst { get; set; }
    public decimal Total { get; set; }
    public string CostCentre { get; set; }
    public string PaymentMethod { get; set; }
    public decimal PreGst => Total - Gst;
}