using damphat.Json.Language;
using Xunit;

namespace damphat.Json.Tests
{
    public class TODO_separators_are_optional
    {
        [Fact]
        public void Semicolon()
        {
            var ret = JSON.Parse("x=1 \n x");
            Assert.Equal(1.0, ret);
        }

        [Theory]
        [InlineData("1 2")]
        [InlineData("[1 2]")]
        [InlineData("x=1; {x x}")]
        public void MissingSeparators(string src)
        {
            Assert.Throws<ParserException>(() => JSON.Parse(src));
        }
    }
}