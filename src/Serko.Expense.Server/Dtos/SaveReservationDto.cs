using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Serko.Expense.Server.Converters;

namespace Serko.Expense.Server.Dtos
{
    public class SaveReservationDto
    {
        [DataMember]
        public string Vendor { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        [JsonConverter(typeof(DateFormatConverter))]
        public DateTime Date { get; set; }

        [DataMember]
        public ExpenseDto Expense { get; set; }
    }
}