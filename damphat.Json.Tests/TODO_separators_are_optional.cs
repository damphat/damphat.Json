using damphat.Json.Language;
using Xunit;

namespace damphat.Json.Tests
{
    public class TODO_separators_are_optional
    {
        [Fact (Skip = "v1")]
        public void Seprator_can_be_replce_with_line_break()
        {
            var ret = JSON.Parse("x=1 \n x");
            Assert.Equal(1.0, ret);
        }

        [Theory (Skip = "v1")]
        [InlineData("1 2")]
        [InlineData("[1 2]")]
        [InlineData("x=1; {x x}")]
        public void Throw_exception_if_neither_linebreak_and_separator(string src)
        {
            Assert.Throws<ParserException>(() => JSON.Parse(src));
        }
    }
}