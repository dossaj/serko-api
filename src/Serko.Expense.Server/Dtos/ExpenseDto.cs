using System.Runtime.Serialization;

namespace Serko.Expense.Server.Dtos
{
    [DataContract(Name = "expense")]
    public class ExpenseDto
    {
        [DataMember(Name = "cost_centre")]
        public string CostCentre { get; set; }

        [DataMember(Name = "payment_method")]
        public string PaymentMethod { get; set; }

        [DataMember]
        public decimal Total { get; set; }
    }
}