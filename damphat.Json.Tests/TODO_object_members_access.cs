using Xunit;

namespace damphat.Json.Tests
{
    public class TODO_object_members_access
    {
        [Fact(Skip = "v1")]
        public void Accessing_fields_with_dot()
        {
            var result = JSON.Parse("a = {x:1, y:2}; a.x + a.y");
            Assert.Equal(3.0, result);
        }
    }
}