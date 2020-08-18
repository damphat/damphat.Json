using System;
using System.Collections;
using System.Globalization;
using System.Text;

namespace damphat.Json
{
    internal static class Utils
    {
        private static void WriteIndent(StringBuilder sb, int indent, int indentLevel)
        {
            sb.AppendLine();
            sb.Append(' ', indentLevel * indent);
        }

        private static char Hex(int c)
        {
            return c < 10 ? (char) ('0' + c) : (char) ('a' + (c - 10));
        }

        private static StringBuilder WriteString(StringBuilder sb, string s)
        {
            sb.Append('"');
            foreach (var c in s)
                if (c >= ' ')
                    switch (c)
                    {
                        case '\"':
                            sb.Append(@"\""");
                            break;
                        case '\\':
                            sb.Append(@"\\");
                            break;
                        default:
                            sb.Append(c);
                            break;
                    }
                else
                    switch (c)
                    {
                        case '\b':
                            sb.Append(@"\b");
                            break;
                        case '\f':
                            sb.Append(@"\f");
                            break;
                        case '\r':
                            sb.Append(@"\r");
                            break;
                        case '\n':
                            sb.Append(@"\n");
                            break;
                        case '\t':
                            sb.Append(@"\t");
                            break;
                        default:
                        {
                            sb.Append(@"\u00");
                            sb.Append(Hex(c / 16));
                            sb.Append(Hex(c % 16));
                            break;
                        }
                    }

            return sb.Append('"');
        }

        private static void WriteKey(StringBuilder sb, string key)
        {
            WriteString(sb, key);
        }

        private static StringBuilder WriteObject(StringBuilder sb, IDictionary dict, int indent, int indentLevel)
        {
            if (dict.Count == 0) indent = 0;
            sb.Append("{");
            var first = true;

            foreach (DictionaryEntry e in dict)
            {
                if (first) first = false;
                else sb.Append(',');
                if (indent > 0) WriteIndent(sb, indent, indentLevel + 1);

                WriteKey(sb, e.Key.ToString());

                sb.Append(':');
                if (indent > 0) sb.Append(' ');

                Write(sb, e.Value, indent, indentLevel + 1);
            }

            if (indent > 0) WriteIndent(sb, indent, indentLevel);
            sb.Append("}");
            return sb;
        }

        private static StringBuilder WriteArray(StringBuilder sb, IEnumerable list, int indent, int indentLevel)
        {
            if (list is ICollection col && col.Count == 0) indent = 0;
            sb.Append("[");
            var first = true;
            foreach (var e in list)
            {
                if (first) first = false;
                else sb.Append(",");

                if (indent > 0)
                    WriteIndent(sb, indent, indentLevel + 1);

                Write(sb, e, indent, indentLevel + 1);
            }

            if (indent > 0) WriteIndent(sb, indent, indentLevel);
            sb.Append("]");
            return sb;
        }

        public static StringBuilder Write(StringBuilder sb, object o, int indent, int indentLevel)
        {
            switch (o)
            {
                case null: return sb.Append("null");
                case bool b: return sb.Append(b ? "true" : "false");
                case string s: return WriteString(sb, s);
                case IDictionary dict: return WriteObject(sb, dict, indent, indentLevel);
                case IEnumerable list: return WriteArray(sb, list, indent, indentLevel);
                default: return sb.Append(Convert.ToString(o, CultureInfo.InvariantCulture));
            }
        }
    }
}