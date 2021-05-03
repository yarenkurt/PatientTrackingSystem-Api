using Core.Helpers;
using Xunit;

namespace UnitTest.Core.Helpers
{
    public class IdentityNumberHelpers
    {
        [Fact]
        public void Verify_True_Status()
        {
            var result = IdentityNumberHelper.Verify("28202193344");
            Assert.True(result);
        }

        [Fact]
        public void Verify_False_Status()
        {
            var result = IdentityNumberHelper.Verify("28202193343");
            Assert.False(result);
        }
    }
}