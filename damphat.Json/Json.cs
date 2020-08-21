using System.Collections.Generic;
using System.Text;
using damphat.Json.Language;

namespace damphat.Json
{
    public static class JSON
    {
        public static string Stringify(object obj, int indent = 0)
        {
            return Writer.Write(new StringBuilder(), obj, indent, 0).ToString();
        }

        public static object Parse(string src, IDictionary<string, object> context = null)
        {
            return new JsonParser(src, context).Parse();
        }
    }
}