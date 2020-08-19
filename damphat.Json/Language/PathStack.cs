using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using damphat.Json.Language.Utils;

namespace damphat.Json.Language
{
    public class PathStack : IReadOnlyList<string>
    {
        private List<string> list = new List<string>();
        private List<int> stack = new List<int>();

        public PathStack()
        {
        }

        public int Start { get; private set; }
        public int End { get; private set; }

        public void Add1(string key)
        {
            list.Add(key);
            End++;
        }

        public void Add(string path)
        {
            if (string.IsNullOrEmpty(path)) return;

            if (Paths.ShouldSplit(path))
            {
                var keys = Paths.Split(path);
                list.AddRange(keys);
                End += keys.Length;
            }
            else
            {
                Add1(path);
            }
        }

        public void Add(object key)
        {
            switch (key)
            {
                case null:
                    Add1("null");
                    break;
                case bool b:
                    Add1(b ? "true" : "false");
                    break;
                case string path:
                    Add(path);
                    break;
                case double d:
                    Add1(d.ToString(CultureInfo.InvariantCulture));
                    break;
                default:
                    Add1(Convert.ToString(key, CultureInfo.InvariantCulture));
                    break;
            }
        }

        public void Push()
        {
            stack.Add(End);
            Start = End;
        }

        public PathStack Pop()
        {
            if (stack.Count == 0) throw new InvalidOperationException("Nothing to pop");

            End = Start;
            list.RemoveRange(End, list.Count - End);
            stack.RemoveAt(stack.Count - 1);

            if (stack.Count > 0)
                Start = stack[stack.Count - 1];
            else
                Start = 0;

            return this;
        }

        public void Clear()
        {
            if (End > Start)
            {
                End = Start;
                list.RemoveRange(End, list.Count - End);
            }
        }

        public IEnumerator<string> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Count => list.Count;

        public string this[int index] => list[index];
    }
}