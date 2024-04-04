using System;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Serko.Expense.Server.Dtos
{
    [DataContract]
    public class ReservationDto
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Vendor { get; set; }

        [JsonPropertyName("cost_centre")]
        [DataMember(Name = "cost_centre")]
        public string CostCentre { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public DateTime Date { get; set; }

        [DataMember]
        public decimal Gst { get; set; }

        [JsonPropertyName("pre_gst")]
        [DataMember(Name = "pre_gst")]
        public decimal PreGst{ get; set; }

        [DataMember]
        public decimal Total { get; set; }

        [JsonPropertyName("payment_method")]
        [DataMember(Name = "payment_method")]
        public string PaymentMethod { get; set; }
    }
}