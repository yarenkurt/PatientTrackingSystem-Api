using Core.Extensions;
using Xunit;

namespace UnitTest.Core.Extensions
{
    public class StringExtensionsTests
    {
        [Theory]
        [InlineData("Wolverine")]
        public void Left_FirstCharacterW_CroppedText(string search)
        {
            Assert.True(search.Left(1) == "W");
        }

        [Theory]
        [InlineData("Thor")]
        public void Right_LastTwoCharacterR_CroppedText(string search)
        {
            Assert.True(search.Right(2) == "or");
        }

        [Theory]
        [InlineData("Loki")]
        public void Reverse_ikoL_CroppedText(string search)
        {
            Assert.True(search.Reverse() == "ikoL");
        }

        [Fact]
        public void StripHtmlTags_Hello_ClearedHtmlTags()
        {
            const string source = "<a src=\"https://www.google.com\">Hello</a>";
            Assert.True(source.StripHtmlTags() == "Hello");
        }

        [Fact]
        public void ClearSymbol_OnlyCharacterAndNumber_ClearedSymbol()
        {
            const string source = "H@e|l lo,-----1*>-+";
            Assert.True(source.ClearSymbol() == "Hello1");
        }

        [Theory]
        [InlineData("Country")]
        [InlineData("Currency")]
        [InlineData("Language")]
        public void Pluralize_Country_Countries(string source)
        {
            var result = source.Pluralize();
            Assert.True(result != source);
        }

        [Fact]
        public void ToBase64String_True_String()
        {
            const string source = "Hello World";
            var encode = source.ToBase64String();

            var decode = encode.FromBase64String();

            Assert.Equal(source, decode);
        }

        [Fact]
        public void FromBase64String_True_String()
        {
            const string source = "Hello World";
            var encode = source.ToBase64String();

            var decode = encode.FromBase64String();

            Assert.Equal(source, decode);
        }
    }
}