using System;
using Core.Extensions;
using Xunit;

namespace UnitTest.Core.Extensions
{
    public class DateTimeExtensionsTests
    {
        [Theory]
        [InlineData("12.30.2020")]
        [InlineData("12/30/2020")]
        [InlineData("12-30-2020")]
        public void ToDate_HasValid_DateTime(string source)
        {
            var date = DateTime.Parse("2020-12-30");

            var result = source.ToDate();

            Assert.Equal(date, result);
        }

        [Theory]
        [InlineData("30.12.2020")]
        [InlineData("30/12/2020")]
        [InlineData("30-12-2020")]
        public void ToDate_HasInValid_DateTime(string source)
        {
            var date = DateTime.Parse("2020-12-30");

            var result = source.ToDate();

            Assert.NotEqual(date, result);
        }

        [Fact]
        public void ToDate_HasInValid_Exception()
        {
            const string source = "30.12.2020";
            var ex = Assert.Throws<Exception>(() => source.ToDate(true));

            Assert.Equal("Cannot Be Converted DateTime",ex.Message);
        }

        [Fact]
        public void Between_BetweenTwoDates_True()
        {
            var date1 = DateTime.Now.Date.AddDays(-1);
            var date2 = DateTime.Now.Date.AddDays(1);

            var result = DateTime.Now.Date.Between(date1, date2);

            Assert.True(result);
        }

        [Fact]
        public void ToReadableTime_Ago_String()
        {
            var result = DateTime.Now.AddMinutes(-3).ToReadableTime();
            Assert.Contains("ago", result);
        }

        [Fact]
        public void ToReadableTime_Later_String()
        {
            var result = DateTime.Now.AddMinutes(3).ToReadableTime();
            Assert.Contains("later", result);
        }
    }
}