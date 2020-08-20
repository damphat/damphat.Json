using Xunit;

namespace damphat.Json.Tests
{
    public class TODO_context
    {
        [Fact]
        public void Do_not_share_variable_between_parse_calls()
        {
            JSON.Parse("x = 1");
            Assert.NotEqual(1.0, JSON.Parse("x"));
        }

        [Fact]
        public void Can_be_an_object_of_generic_IDictionary()
        {
            // IDictionary<string, object> context = new Dictionary<string, object>();
            // JSON.Parse("", context)
            Assert.True(false);
        }

        [Fact(Skip = "big idea")]
        public void Idea_engine_context_proto()
        {
            // var context = new {PI = 3.14};
            // var ctx = JSON.CreateEngine(context);
            // ctx.Eval("x = 1");
            // Assert.Equal(ctx["x"], ctx.Eval("x"));
        }
    }
}