using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Serko.Expense.Server.Dtos
{

    [DataContract(Name = "expense", Namespace = "")]
    public class ExpenseDto
    {
        [XmlElement("cost_centre")]
        [DataMember(Name = "cost_centre")]
        public string CostCentre { get; set; }

        [XmlElement("payment_method")]
        [DataMember(Name = "payment_method")]
        public string PaymentMethod { get; set; }

        [DataMember]
        [XmlElement("total")]
        public decimal Total { get; set; }
    }
}