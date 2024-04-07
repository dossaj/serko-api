using System;
using Serko.Expense.Server.Extensions;
using Xunit;

namespace Serko.Expense.Tests;

public class DateTimeConverterTests
{
    [Fact]
    public void ToDateTime_CustomDateFormat_DateTimeIsParsedCorrectly()
    {
        //arrange
        var converter = "Thursday 27 April 2017";

        //act
        var result = converter.ToDateTime();

        //assert
        Assert.Equal(new DateTime(2017, 4, 27), result);
    }

    [Fact]
    public void ToDateTime_DefaultDateFormat_DateTimeIsParsedCorrectly()
    {
        //arrange
        var converter = "2017-04-27";

        //act
        var result = converter.ToDateTime();

        //assert
        Assert.Equal(new DateTime(2017, 4, 27), result);
    }
}