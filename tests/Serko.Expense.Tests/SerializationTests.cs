﻿using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Serko.Expense.Core.Serialization;
using Serko.Expense.Server.Dtos;
using Xunit;

namespace Serko.Expense.Tests;

public class SerializationTests
{
    [Fact]
    public void Serialization_CorrectContent_ValuesDeserializedCorrectly()
    {
        string xml = @"<SaveReservationDto><expense><cost_centre>DEV002</cost_centre>
    <total>1024.01</total><payment_method>personal card</payment_method>
</expense><vendor>Viaduct Steakhouse</vendor><description>development team’s project end celebration dinner</description><date>Thursday 27 April 2017</date></SaveReservationDto>";

        using var stream = new StringReader(xml);
        using var reader = XmlReader.Create(stream);

        var serializer = new XmlSerializer(typeof(SaveReservationDto));
        var model = (SaveReservationDto)serializer.Deserialize(reader);

        Assert.Equal("Thursday 27 April 2017", model.Date);
        Assert.Equal(new DateTime(2017, 4, 27), model.DateTime);
        Assert.Equal("Viaduct Steakhouse", model.Vendor);
        Assert.Equal("DEV002", model.Expense.CostCentre);
        Assert.Equal(1024.01m, model.Expense.Total);
    }

    [Fact]
    public async Task Serialization_EmailContent_ValuesDeserializedCorrectly()
    {
        using var email = Assembly
            .GetExecutingAssembly()
            .GetManifestResourceStream("Serko.Expense.Tests.Resources.Email");

        using var stream = new StreamReader(email);
        var lexer = await new EmailXmlLexer(stream).ToArrayAsync();

        using var text = new EmailXmlTextReader(lexer, typeof(SaveReservationDto));
        using var reader = XmlReader.Create(text);
        
        var serializer = new XmlSerializer(typeof(SaveReservationDto));
        var model = (SaveReservationDto)serializer.Deserialize(reader);

        Assert.Equal("Thursday 27 April 2017", model.Date);
        Assert.Equal(new DateTime(2017, 4, 27), model.DateTime);
        Assert.Equal("Viaduct Steakhouse", model.Vendor);
        Assert.Equal("DEV002", model.Expense.CostCentre);
        Assert.Equal(1024.01m, model.Expense.Total);
        
    }

    [Fact]
    public async Task Serialization_EmailContentWithEmailAddress_ValuesDeserializedCorrectly()
    {
        var email = Assembly
            .GetExecutingAssembly()
            .GetManifestResourceStream("Serko.Expense.Tests.Resources.EmailTo");

        using var stream = new StreamReader(email);
        var lexer = await new EmailXmlLexer(stream).ToArrayAsync();

        using var text = new EmailXmlTextReader(lexer, typeof(SaveReservationDto));
        using var reader = XmlReader.Create(text);
                    
        var serializer = new XmlSerializer(typeof(SaveReservationDto));
        var model = (SaveReservationDto)serializer.Deserialize(reader);

        Assert.Equal("Thursday 27 April 2017", model.Date);
        Assert.Equal(new DateTime(2017, 4, 27), model.DateTime);
        Assert.Equal("Viaduct Steakhouse", model.Vendor);
        Assert.Equal("DEV002", model.Expense.CostCentre);
        Assert.Equal(1024.01m, model.Expense.Total);
    }
}
