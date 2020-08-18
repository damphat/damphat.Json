using System;
using System.Text;

namespace damphat.Json
{
    public static class JSON
    {
        public static string Stringify(object o, int indent = 0)
        {
            return Utils.Write(new StringBuilder(), o, indent, 0).ToString();
        }

        public static object Parse(string src)
        {
            throw new NotImplementedException();
        }
    }
}