using System;
using System.Collections.Generic;
using static System.Console;

namespace damphat.Json.Console
{
    internal class Program
    {
        private static Dictionary<string, object> context = new Dictionary<string, object>();

        private static void Main(string[] args)
        {
            while (true)
            {
                Write('>');
                var src = ReadLine();
                if (src == null || src == "exit") break;
                switch (src)
                {
                    case "cls":
                    case "clear":
                        Clear();
                        continue;
                    case "":
                        continue;
                    case "dir":
                        WriteLine(JSON.Stringify(context, 2));
                        continue;
                    default:
                        try
                        {
                            var obj = JSON.Parse(src, context);

                            WriteLine(JSON.Stringify(obj, 2));
                        }
                        catch (Exception e)
                        {
                            Error.WriteLine(e.Message);
                        }

                        break;
                }
            }
        }
    }
}