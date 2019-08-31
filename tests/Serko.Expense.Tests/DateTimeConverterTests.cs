using System;
using Newtonsoft.Json;
using Serko.Expense.Server.Converters;
using Serko.Expense.Server.Dtos;
using Xunit;

namespace Serko.Expense.Tests
{
    public class DateTimeConverterTests
    {
        [Fact]
        public void ReadJson_CustomDateFormat_DateTimeIsParsedCorrectly()
        {
            //arrange
            var converter = new DateFormatConverter();
            var json = "{ \"date\": \"Thursday 27 April 2017\" }";

            //act

            var result = JsonConvert.DeserializeObject<SaveReservationDto>(json, converter);

            //assert
            Assert.Equal(new DateTime(2017, 4, 27), result.Date);
        }

        [Fact]
        public void ReadJson_DefaultDateFormat_DateTimeIsParsedCorrectly()
        {
            //arrange
            var converter = new DateFormatConverter();
            var json = "{ \"date\": \"2017-04-27\" }";

            //act

            var result = JsonConvert.DeserializeObject<SaveReservationDto>(json, converter);

            //assert
            Assert.Equal(new DateTime(2017, 4, 27), result.Date);
        }
    }
}
