using Xunit;

namespace damphat.Json.Tests
{
    public class JsonTests
    {
        [Theory]
        [InlineData(null, "null")]
        [InlineData(true, "true")]
        [InlineData(false, "false")]
        [InlineData(-90.09, "-90.09")]
        [InlineData(double.PositiveInfinity, "Infinity")]
        [InlineData(double.NegativeInfinity, "-Infinity")]
        [InlineData(double.NaN, "NaN")]
        public void StringifyPrimitive(object value, string expected)
        {
            Assert.Equal(expected, JSON.Stringify(value));
        }
    }
}