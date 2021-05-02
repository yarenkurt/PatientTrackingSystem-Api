using Core.Extensions;
using Xunit;

namespace UnitTest.Core.Extensions
{
    public class NumericExtensionsTests
    {
        [Theory]
        [InlineData("123")]
        [InlineData("1234567890123456789")]
        public void IsNumeric_True_Boolean(string source)
        {
            Assert.True(source.IsNumeric());
        }


        
    }
}