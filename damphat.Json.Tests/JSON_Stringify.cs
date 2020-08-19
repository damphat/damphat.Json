using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace damphat.Json.Tests
{
    public class JSON_Stringify
    {
        [Fact]
        public void Null()
        {
            Assert.Equal("null", JSON.Stringify(null));
        }

        [Theory]
        [InlineData(false, "false")]
        [InlineData(true, "true")]
        public void Bool(object value, string expected)
        {
            Assert.Equal(expected, JSON.Stringify(value));
        }

        [Theory]
        [InlineData(0, "0")]
        [InlineData(-90.09, "-90.09")]
        [InlineData(double.PositiveInfinity, "Infinity")]
        [InlineData(double.NegativeInfinity, "-Infinity")]
        [InlineData(double.NaN, "NaN")]
        public void Number(object value, string expected)
        {
            Assert.Equal(expected, JSON.Stringify(value));
        }

        [Theory]
        [InlineData("abc", @"""abc""")]
        [InlineData("\b\f\r\n\t", @"""\b\f\r\n\t""")]
        [InlineData("\u0000", @"""\u0000""")]
        [InlineData("\"\\", @"""\""\\""")]
        public void String(string value, string expected)
        {
            Assert.Equal(expected, JSON.Stringify(value));
        }

        [Fact]
        public void IEnumerable()
        {
            Assert.Equal("[]", JSON.Stringify(new object[] { }));
            Assert.Equal("[1]", JSON.Stringify(new int[] {1}));
            Assert.Equal("[1,2]", JSON.Stringify(new double[] {1, 2}));
            Assert.Equal("[[]]", JSON.Stringify(new List<object> {new List<object>()}));
        }

        [Fact]
        public void IDictionary()
        {
            Assert.Equal("{}", JSON.Stringify(new Hashtable()));
            Assert.Equal(@"{""x"":1}", JSON.Stringify(new Hashtable {{"x", 1}}));
            Assert.Equal(@"{""x"":1,""y"":2}", JSON.Stringify(new Dictionary<object, object>() {{"x", 1}, {"y", 2}}));
            Assert.Equal(@"{""x"":{""y"":2}}", JSON.Stringify(new Hashtable {{"x", new Hashtable {{"y", 2}}}}));
        }

        [Fact]
        public void Pretty_with_indent()
        {
            var city = new Dictionary<string, object>
            {
                {"name", "Saigon"},
                {
                    "location", new List<double>
                    {
                        10,
                        100
                    }
                }
            };

            var expected = new string[]
            {
                "{",
                "  'name': 'Saigon',",
                "  'location': [",
                "    10,",
                "    100",
                "  ]",
                "}"
            };


            Assert.Equal(string.Join(Environment.NewLine, expected).Replace('\'', '"'), JSON.Stringify(city, 2));
        }

        [Fact(Skip = "not supported yet")]
        public void AnonymousTypes()
        {
            var name = new {first = "A", last = "B"};
            Assert.Equal(@"{""first"":""A"",""last"":""B""}", JSON.Stringify(name));
        }
    }
}