namespace damphat.Json.Language
{
    public class TokenError
    {
        public TokenErrorKind Kind { get; }
        public int At { get; }

        public TokenError(TokenErrorKind kind, int at)
        {
            Kind = kind;
            At = at;
        }
    }
}