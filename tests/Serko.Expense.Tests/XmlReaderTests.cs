using System;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;
using Serko.Expense.Server.Dtos;
using Serko.Expense.Server.Formatters;
using Xunit;

namespace Serko.Expense.Tests
{
    public class XmlReaderTests
    {
        [Fact]
        public void DoIt()
        {
            string xml = @"<SaveReservationDto><expense><cost_centre>DEV002</cost_centre>
    <total>1024.01</total><payment_method>personal card</payment_method>
</expense><vendor>Viaduct Steakhouse</vendor><description>development team’s project end celebration dinner</description><date>Thursday 27 April 2017</date></SaveReservationDto>";

            using (var stream = new StringReader(xml))
            using (var reader = XmlReader.Create(stream))
            {
                var serializer = new XmlSerializer(typeof(SaveReservationDto));
                var model = (SaveReservationDto)serializer.Deserialize(reader);

                Assert.Equal("Thursday 27 April 2017", model.Date);
                Assert.Equal(new DateTime(2017, 4, 27), model.DateTime);
                Assert.Equal("Viaduct Steakhouse", model.Vendor);
                Assert.Equal("DEV002", model.Expense.CostCentre);
                Assert.Equal(1024.01m, model.Expense.Total);
            }
        }

        [Fact]
        public void DoIt2()
        {
            var email = Assembly
                .GetExecutingAssembly()
                .GetManifestResourceStream("Serko.Expense.Tests.Resources.Email");

            using (var stream = new CustomTextReader(email, typeof(SaveReservationDto)))
            using (var reader = XmlReader.Create(stream))
            {
                var serializer = new XmlSerializer(typeof(SaveReservationDto));
                var model = (SaveReservationDto)serializer.Deserialize(reader);

                Assert.Equal("Thursday 27 April 2017", model.Date);
                Assert.Equal(new DateTime(2017, 4, 27), model.DateTime);
                Assert.Equal("Viaduct Steakhouse", model.Vendor);
                Assert.Equal("DEV002", model.Expense.CostCentre);
                Assert.Equal(1024.01m, model.Expense.Total);
            }
        }

        [Fact]
        public void DoIt3()
        {
            var email = Assembly
                .GetExecutingAssembly()
                .GetManifestResourceStream("Serko.Expense.Tests.Resources.EmailTo");

            using (var stream = new CustomTextReader(email, typeof(SaveReservationDto)))
            using (var reader = XmlReader.Create(stream))
            {
                var serializer = new XmlSerializer(typeof(SaveReservationDto));
                var model = (SaveReservationDto)serializer.Deserialize(reader);

                Assert.Equal("Thursday 27 April 2017", model.Date);
                Assert.Equal(new DateTime(2017, 4, 27), model.DateTime);
                Assert.Equal("Viaduct Steakhouse", model.Vendor);
                Assert.Equal("DEV002", model.Expense.CostCentre);
                Assert.Equal(1024.01m, model.Expense.Total);
            }
        }
    }
}
