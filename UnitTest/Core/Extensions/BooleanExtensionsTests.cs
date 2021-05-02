using Core.Extensions;
using Xunit;

namespace UnitTest.Core.Extensions
{
    public class BooleanExtensionsTests
    {
        [Theory]
        [InlineData("TRUE")]
        [InlineData("TrUe")]
        [InlineData("1")]
        public void ToBool_True_Boolean(string source)
        {
            var result = source.ToBool();
            Assert.True(result);
        }

        [Theory]
        [InlineData("FALSE")]
        [InlineData("FaLsE")]
        [InlineData("0")]
        [InlineData("")]
        [InlineData(null)]
        public void ToBool_False_Boolean(string source)
        {
            var result = source.ToBool();
            Assert.False(result);
        }
    }
}