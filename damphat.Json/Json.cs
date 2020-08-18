using System;

namespace damphat.Json
{
    public static class JSON
    {
        public static string Stringify(object o)
        {
            return o.ToJson();
        }

        public static object Parse(string src)
        {
            throw new NotImplementedException();
        }
    }
}
