using System;
using System.Collections.Generic;
using Xunit;

namespace damphat.Json.Tests
{
    public class TODO_invoke_syntax
    {
        [Fact(Skip = "v1")]
        public void InvokeFunc()
        {
            var context = new Dictionary<string, object>();
            context["pow"] = new Func<double, double, double>((x, y) => Math.Pow(x, y));

            var actual = JSON.Parse("pow(2, 3)", context);

            Assert.Equal(8.0, actual);
        }

        [Fact(Skip = "v1")]
        public void InvokeMethod()
        {
            var actual = JSON.Parse("x = 'a';  x.ToUpper()");

            Assert.Equal("A", actual);
        }
    }
}