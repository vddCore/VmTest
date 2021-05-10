namespace VmTest.Lexical
{
    public class Token
    {
        public TokenType Type { get; }
        public object Value { get; }

        public Token(TokenType type)
        {
            Type = type;
        }

        public Token(TokenType type, object value)
            : this(type)
        {
            Value = value;
        }
    }
}