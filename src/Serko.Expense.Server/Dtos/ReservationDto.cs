using System;

namespace Serko.Expense.Server.Dtos
{
    public class ReservationDto
    {
        public int Id { get; set; }
        public string Vendor { get; set; }
        public string CostCentre { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public decimal Gst { get; set; }
        public decimal PreGst{ get; set; }
        public decimal Total { get; set; }
        public string PaymentMethod { get; set; }
    }
}