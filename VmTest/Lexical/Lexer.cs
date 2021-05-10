using System;

namespace VmTest.Lexical
{
    public class Lexer
    {
        private string _input;

        public int Position { get; set; }

        public Lexer(string input)
        {
            _input = input;
        }

        public Token Next()
        {
            if (Position >= _input.Length)
                return new Token(TokenType.EOF);
            
            ConsumeWhitespace();

            if (char.IsDigit(_input[Position]))
            {
                return new Token(TokenType.Number, GetNumber());
            }
            else if (_input[Position] == '+')
            {
                Advance();
                return new Token(TokenType.Plus);
            }
            else if (_input[Position] == '-')
            {
                Advance();
                return new Token(TokenType.Minus);
            }
            else if (_input[Position] == '*')
            {
                Advance();
                return new Token(TokenType.Star);
            }
            else if (_input[Position] == '/')
            {
                Advance();
                return new Token(TokenType.Slash);
            }
            else if (_input[Position] == '(')
            {
                Advance();
                return new Token(TokenType.LParen);
            }
            else if (_input[Position] == ')')
            {
                Advance();
                return new Token(TokenType.RParen);
            }
            else throw new Exception($"Unexpected token '{_input[Position]}'.");
        }

        public decimal GetNumber()
        {
            var str = string.Empty;
            var dotEncountered = false;

            while (Position < _input.Length && (char.IsDigit(_input[Position]) || _input[Position] == '.'))
            {
                if (_input[Position] == '.')
                {
                    if (dotEncountered)
                        throw new Exception("Invalid decimal number provided.");

                    dotEncountered = true;
                }

                str += _input[Position++];
            }

            return decimal.Parse(str);
        }

        private void ConsumeWhitespace()
        {
            while (char.IsWhiteSpace(_input[Position]))
                Position++;
        }

        public void Advance()
        {
            Position++;
        }
    }
}