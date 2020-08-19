using System.Collections.Generic;
using damphat.Json.Language.Utils;

namespace damphat.Json.Language
{
    using Dict = Dictionary<string, object>;

    internal class JsonParser
    {
        public static Dict DefaultContext = new Dict();
        private Dict context;
        private readonly PathStack path = new PathStack();

        private readonly PeekableLexer lexer;
        private Token t;

        public JsonParser(string src, Dict context)
        {
            lexer = new PeekableLexer(src, 0, 1);
            t = lexer.Peek(0);
            this.context = context ?? DefaultContext;
        }

        private void Next()
        {
            lexer.Move(1);
            t = lexer.Peek(0);
        }

        private static string KindToString(Kind kind)
        {
            switch (kind)
            {
                case Kind.Open: return "'{'";
                case Kind.Close: return "'}'";
                case Kind.OpenBracket: return "'['";
                case Kind.CloseBracket: return "']'";
                case Kind.OpenParens: return "'('";
                case Kind.CloseParens: return "')'";
                case Kind.Comma: return "','";
                case Kind.Colon: return "':'";
                case Kind.SemiColon: return "';'";
                default: return kind.ToString();
            }
        }

        private ParserException Expect(Kind kind)
        {
            return Expect(KindToString(kind));
        }

        private ParserException Expect(string what)
        {
            if (t.Kind == Kind.Eof)
                return new ParserException($"{what} expected at {t.Start}. Unexpected end of input");
            else
                return new ParserException($"{what} expected, found '{t.Raw}' at {t.Start}");
        }

        private bool Eof()
        {
            return t.Kind == Kind.Eof;
        }

        private void Require(Kind kind)
        {
            if (t.Kind != kind) throw Expect(kind);

            Next();
        }

        private Kind EatKind()
        {
            var ret = t.Kind;
            Next();
            return ret;
        }

        private object EatValue()
        {
            if (t.Errors != null) throw new ParserException($"{t.Errors[0].Kind.ToStringEx()} at {t.Errors[0].At}");

            var ret = t.Value;
            Next();
            return ret;
        }

        private object Eat(object v)
        {
            Next();
            return v;
        }

        public object Factor()
        {
            object Parens()
            {
                Next();
                var expr = Expr();
                Require(Kind.CloseParens);
                return expr;
            }

            switch (t.Kind)
            {
                case Kind.Minus:
                case Kind.Plus: return Operators.Unary(EatKind(), Factor());
                case Kind.Number: return EatValue();
                case Kind.String: return EatValue();
                case Kind.Identifier:
                    switch (t.Raw)
                    {
                        case "null": return Eat(null);
                        case "true": return Eat(true);
                        case "false": return Eat(false);
                        case "Infinity": return Eat(double.PositiveInfinity);
                        case "NaN": return Eat(double.NaN);
                        // TODO not found
                        default:
                        {
                            var key = (string) Eat(t.Raw);
                            return context.TryGetValue(key, out var value) ? value : key;
                        }
                    }
                case Kind.OpenParens: return Parens();
                case Kind.OpenBracket: return ParseArray();
                case Kind.Open: return ParseObject();
                default:
                    throw Expect("value");
            }
        }

        // for both array and object
        private bool Sep()
        {
            if (t.Kind == Kind.Comma || t.Kind == Kind.SemiColon)
            {
                Next();
                return true;
            }

            return t.Separator > 0;
        }

        private object ParseArray()
        {
            bool Close()
            {
                if (t.Kind == Kind.CloseBracket)
                {
                    Next();
                    return true;
                }

                return false;
            }

            Next();
            var list = new List<object>();
            if (Close()) return list;

            while (true)
            {
                list.Add(Expr());
                if (Close()) return list;

                if (Sep())
                {
                    if (Close())
                    {
                        return list;
                    }
                    else
                    {
                        /*loop*/
                    }
                }
                else
                {
                    throw Expect("','");
                }
            }
        }

        private void ReadKey()
        {
            switch (t.Kind)
            {
                case Kind.String:
                    path.Add((string) EatValue());
                    break;
                case Kind.Identifier:
                    path.Add1((string) Eat(t.Raw));
                    break;
                case Kind.Number:
                    path.Add1(Operators.ToStr((double) EatValue()));
                    break;
                default:
                    throw Expect("key");
            }
        }

        private void ReadPath()
        {
            bool KeySep()
            {
                if (t.Kind == Kind.Div)
                {
                    Next();
                    return true;
                }

                return false;
            }

            path.Clear();
            ReadKey();
            while (KeySep()) ReadKey();
        }


        private static Dict SetN(Dict dict, IReadOnlyList<string> path, object value, int start)
        {
            switch (path.Count - start)
            {
                case 1:
                    dict.Add(path[start], value);
                    return dict;
                default:
                {
                    var k = path[start];
                    var v = SetN(new Dict(), path, value, start + 1);
                    dict.Add(k, v);
                    return dict;
                }
            }
        }

        private Dict ReadPathValue(Dict obj)
        {
            bool Colon()
            {
                if (t.Kind == Kind.Colon)
                {
                    Next();
                    return true;
                }

                return false;
            }

            ReadPath();
            if (Colon())
            {
                var value = Expr();
                SetN(obj, path, value, path.Start);

                return obj;
            }
            else
            {
                var value = context[path[path.Count - 1]];
                SetN(obj, path, value, path.Start);
                return obj;
            }
        }

        private object ParseObject()
        {
            Dict ParseFlag()
            {
                var raw = t.Raw;
                var k1 = lexer.Peek(1).Kind;
                if (raw.Length > 0 && raw[0] >= '0' && raw[0] <= '7' && k1 != Kind.Colon && k1 != Kind.Div)
                {
                    Next();
                    return new Dict {{"$", raw}};
                }

                return null;
            }

            bool Close()
            {
                if (t.Kind == Kind.Close)
                {
                    Next();
                    return true;
                }

                return false;
            }

            Next();
            var obj = ParseFlag();
            if (obj == null)
            {
                obj = new Dict();
                if (Close())
                {
                    return obj;
                }
                else
                {
                    /*loop*/
                }
            }
            else
            {
                if (Sep())
                {
                    if (Close())
                    {
                        return obj;
                    }
                    else
                    {
                        /*loop*/
                    }
                }
                else
                {
                    if (Close())
                        return obj;
                    else
                        throw Expect("','");
                }
            }

            path.Push();
            try
            {
                while (true)
                {
                    obj = ReadPathValue(obj);
                    if (Close()) return obj;

                    if (Sep())
                    {
                        if (Close())
                        {
                            return obj;
                        }
                        else
                        {
                            /*loop*/
                        }
                    }
                    else
                    {
                        throw Expect("','");
                    }
                }
            }
            finally
            {
                path.Pop();
            }
        }

        public object MultiplicativeExpr()
        {
            var factor = Factor();
            while (true)
                switch (t.Kind)
                {
                    case Kind.Mul:
                    case Kind.Div:
                        factor = Operators.Binary(EatKind(), factor, Factor());
                        break;
                    default:
                        return factor;
                }
        }

        public object AdditiveExpr()
        {
            var ret = MultiplicativeExpr();
            while (true)
                switch (t.Kind)
                {
                    case Kind.Plus:
                    case Kind.Minus:
                        ret = Operators.Binary(EatKind(), ret, MultiplicativeExpr());
                        break;
                    default:
                        return ret;
                }
        }

        private object AssignExpr()
        {
            var key = t.Raw;
            Next();
            Next();
            var value = Expr();
            context[key] = value;
            return value;
        }

        private object Expr()
        {
            if (t.Kind == Kind.Identifier)
                if (lexer.Peek(1).Kind == Kind.Eq)
                    return AssignExpr();

            return AdditiveExpr();
        }

        private object Program()
        {
            bool ExprSep()
            {
                if (t.Kind == Kind.SemiColon)
                {
                    Next();
                    return true;
                }

                return false;
            }

            object last = null;
            if (Eof()) return last;

            while (true)
            {
                last = Expr();
                if (Eof()) return last;

                if (ExprSep())
                {
                    if (Eof()) return last;

                    continue;
                }

                throw Expect("';'");
            }
        }

        public object Parse()
        {
            return Program();
        }
    }
}