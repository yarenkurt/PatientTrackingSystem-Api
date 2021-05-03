using Core.Enums;
using Core.Helpers;
using Xunit;

namespace UnitTest.Core.Helpers
{
    public class EnumTests
    {
        [Fact]
        public void List_NotNull_Dictionary()
        {
            var result = EnumHelper.List<PersonType>();
            Assert.NotNull(result);
        }

        [Fact]
        public void ToList_NotNull_SelectList()
        {
            var result = EnumHelper.List<PersonType>();
            Assert.True(result.ToList().Count > 0);
        }

        [Fact]
        public void GetDisplayValue_NotNull_String()
        {
            var result = EnumHelper.GetDisplayValue(PersonType.Patient);
            Assert.NotNull(result);
        }
    }
}