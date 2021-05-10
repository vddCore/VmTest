using System;
using System.Collections.Generic;
using VmTest.Lexical;

namespace VmTest.Grammar
{
    public class Parser
    {
        private Lexer _lexer;
        private Token _currentToken;

        private List<TokenType> _termOperators = new()
        {
            TokenType.Star,
            TokenType.Slash
        };

        private List<TokenType> _factorOperator = new()
        {
            TokenType.Minus,
            TokenType.Plus
        };
        
        public Parser(string expr)
        {
            _lexer = new Lexer(expr);
            _currentToken = _lexer.Next();
        }

        public AstNode Parse()
        {
            return Factor();
        }

        private AstNode Factor()
        {
            var node = Term();

            while (_factorOperator.Contains(_currentToken.Type))
            {
                var tok = _currentToken;
                if (tok.Type == TokenType.Plus)
                {
                    Match(TokenType.Plus);
                    node = new BinaryOpNode(Term(), node, BinaryOpNode.BinaryOp.Add);
                }
                else if (tok.Type == TokenType.Minus)
                {
                    Match(TokenType.Minus);
                    node = new BinaryOpNode(Term(), node, BinaryOpNode.BinaryOp.Sub);
                }
            }

            return node;
        }

        private AstNode Term()
        {
            var node = Terminal();
            while (_termOperators.Contains(_currentToken.Type))
            {
                var tok = _currentToken;
                if (tok.Type == TokenType.Star)
                {
                    Match(TokenType.Star);
                    node = new BinaryOpNode(Terminal(), node, BinaryOpNode.BinaryOp.Mul);
                }
                else if (tok.Type == TokenType.Slash)
                {
                    Match(TokenType.Slash);
                    node = new BinaryOpNode(Terminal(), node, BinaryOpNode.BinaryOp.Div);
                }
            }

            return node;
        }
        
        private AstNode Terminal()
        {
            var tok = _currentToken;
            if (tok.Type == TokenType.Number)
            {
                Match(TokenType.Number);
                return new NumberNode((decimal)tok.Value);
            }
            else if (tok.Type == TokenType.Minus)
            {
                Match(TokenType.Minus);
                return new UnaryOpNode(Terminal(), UnaryOpNode.UnaryOp.Minus);
            }
            else if (tok.Type == TokenType.Plus)
            {
                Match(TokenType.Plus);
                return new UnaryOpNode(Terminal(), UnaryOpNode.UnaryOp.Plus);
            }
            else if (tok.Type == TokenType.LParen)
            {
                Match(TokenType.LParen);
                var node = Factor();
                Match(TokenType.RParen);

                return node;
            }
            else throw new Exception($"Unexpected token {tok.Type.ToString().ToUpper()}");
        }

        private void Match(TokenType tokenType)
        {
            if (_currentToken.Type != tokenType)
                throw new Exception($"Unexpected token {_currentToken.Type.ToString().ToUpper()}");

            _currentToken = _lexer.Next();
        }
    }
}