using damphat.Json.Language;
using Xunit;

namespace damphat.Json.Tests
{
    public class TODO_dont_turn_identifier_to_string
    {
        [Fact(Skip = "v1")]
        public void Get_unknown_identifier_throw_exception()
        {
            Assert.Throws<ParserException>(() => JSON.Parse("name"));
        }
    }
}