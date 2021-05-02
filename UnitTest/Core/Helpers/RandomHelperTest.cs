using Core.Extensions;
using Core.Helpers;
using Xunit;

namespace UnitTest.Core.Helpers
{
    public class RandomHelperTest
    {
        [Fact]
        public void Numeric_True_String()
        {
            var result = RandomHelper.Numeric(5);
            Assert.True(result.ToInt() > 0);
            Assert.True(result.Length == 5);
        }

        [Fact]
        public void Character_True_String()
        {
            var result = RandomHelper.Character(20);
            Assert.True(result.Length == 20);
        }

        [Fact]
        public void Mixed_True_String()
        {
            var result = RandomHelper.Character(50);
            Assert.True(result.Length == 50);
        }
    }
}