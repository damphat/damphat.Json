using System.Collections;
using System.Collections.Generic;
using damphat.Json.Language;
using Xunit;

namespace damphat.Json.Tests
{
    public class TODO_object_path
    {
        [Fact]
        public void Allow_path_if_not_overwrite_value()
        {
            var actual = JSON.Parse("{a/x:1, a/y:2}");
            var expected = JSON.Parse("{a: {x:1, y:2}}");
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Do_not_split_a_path_of_string()
        {
            var obj = (IDictionary) JSON.Parse("{'a/x':1}");
            Assert.Equal(1.0, obj["a/x"]);
        }

        [Fact]
        public void Shorthand()
        {
            var context = new Dictionary<string, object> {{"x", 1.0}};
            // {x} => {x:x}
            Assert.Equal(JSON.Parse("{x:x}", context), JSON.Parse("{x}", context));
        }

        [Fact]
        public void Shorthand_for_path_is_not_allow()
        {
            var context = new Dictionary<string, object> {{"x", 1.0}};
            // {x} => {x:x}
            Assert.Throws<ParserException>(() => JSON.Parse("{a/x}", context));
        }
    }
}