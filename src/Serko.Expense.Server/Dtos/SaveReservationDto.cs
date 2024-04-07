using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Serko.Expense.Server.Extensions;

namespace Serko.Expense.Server.Dtos;

[DataContract(Namespace = "")]
public class SaveReservationDto
{
    [DataMember]
    [XmlElement("expense")]
    public ExpenseDto Expense { get; set; }

    [DataMember]
    [XmlElement("vendor")]
    public string Vendor { get; set; }

    [DataMember]
    [XmlElement("description")]
    public string Description { get; set; }

    [XmlIgnore]
    [IgnoreDataMember]
    public DateTime DateTime => Date.ToDateTime("dddd d MMMM yyyy");

    [DataMember]
    [XmlElement("date")]
    public string Date { get; set; }
}