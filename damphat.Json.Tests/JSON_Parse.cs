using System.Collections.Generic;
using System.Linq;
using damphat.Json.Language;
using Xunit;

namespace damphat.Json.Tests
{
    public class JSON_Parse
    {
        [Theory]
        [InlineData("null", null)]
        [InlineData("true", true)]
        [InlineData("false", false)]
        [InlineData("Infinity", double.PositiveInfinity)]
        [InlineData("NaN", double.NaN)]
        [InlineData("123", 123.0)]
        [InlineData("'aaa'", "aaa")]
        [InlineData(@"""aaa""", "aaa")]
        [InlineData("aaa", "aaa")]
        public void Primitives(string src, object result)
        {
            Assert.Equal(result, JSON.Parse(src));
        }

        [Theory]
        [InlineData("[]")]
        [InlineData("[1]")]
        [InlineData("[1,]")]
        [InlineData("[1,2]")]
        [InlineData("[1,2,]")]
        [InlineData("[ 1 2 ]")]
        [InlineData("[1;2;]")]
        public void Arrays(string src)
        {
            var result = src.Where(char.IsDigit).Select(c => double.Parse(c.ToString()));
            Assert.Equal(result, JSON.Parse(src));
        }

        [Theory]
        [InlineData("[")]
        [InlineData("[1")]
        [InlineData("[,]")]
        [InlineData("[,1]")]
        [InlineData("[1,,]")]
        [InlineData("[1,,2]")]
        [InlineData("[1null]")]
        public void Arrays_throw_exception(string src)
        {
            Assert.Throws<ParserException>(() => JSON.Parse(src));
        }

        [Theory]
        [InlineData("//line1\n//line2\n")]
        [InlineData("/**/")]
        [InlineData("/***/")]
        [InlineData("/*/*/")]
        [InlineData("/*//*/")]
        public void Comments(string src)
        {
            Assert.Equal(1.0, JSON.Parse(src + "1"));
        }

        [Theory]
        [InlineData("{}")]
        [InlineData("{x:1}")]
        [InlineData("{x:1,}")]
        [InlineData("x=1; {x}")]
        [InlineData("{x:1 y:2}")]
        [InlineData("{x:1,y:2}")]
        [InlineData("{'x':1}")]
        [InlineData(@"{""x"":1}")]
        public void Objects(string src)
        {
            var expected = new Dictionary<string, object>();
            if (src.Contains('x')) expected.Add("x", 1.0);
            if (src.Contains('y')) expected.Add("y", 2.0);

            Assert.Equal(expected, JSON.Parse(src));
        }

        [Theory]
        [InlineData("{")]
        [InlineData("{x:1")]
        [InlineData("{,}")]
        [InlineData("{,x:1")]
        [InlineData("{x:1,,}")]
        [InlineData("{x:1,,y:2}")]
        [InlineData("{1:1y:2}")]
        public void Objects_throw_exception(string src)
        {
            Assert.Throws<ParserException>(() => JSON.Parse(src));
        }

        [Theory]
        [InlineData("1; 2; 3", 3.0)]
        [InlineData("x=1; y=2; x-y", -1.0)]
        public void Return_last_expression(string src, object result)
        {
            Assert.Equal(result, JSON.Parse(src));
        }
    }
}