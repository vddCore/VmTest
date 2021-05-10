using System;
using System.Collections.Generic;
using VmTest.ExecutionEngine;
using VmTest.Grammar;

namespace VmTest.Compiler
{
    public class CodeGenerator
    {
        private AstNode _expression;

        private List<byte> _program = new();
        
        public CodeGenerator(string expr)
        {
            var parser = new Parser(expr);
            _expression = parser.Parse();
        }

        public byte[] Compile()
        {
            Visit(_expression);
            EmitStor();
            
            return _program.ToArray();
        }

        private void Visit(AstNode astNode)
        {
            if (astNode is NumberNode nn)
            {
                Visit(nn);
            }
            else if (astNode is BinaryOpNode bon)
            {
                Visit(bon);
            }
            else if (astNode is UnaryOpNode uon)
            {
                Visit(uon);
            }
        }

        private void Visit(NumberNode numberNode)
        {
            EmitLoad(numberNode.Value);
        }
        
        private void Visit(BinaryOpNode binaryOpNode)
        {
            Visit(binaryOpNode.Left);
            Visit(binaryOpNode.Right);

            switch (binaryOpNode.Operation)
            {
                case BinaryOpNode.BinaryOp.Add:
                    EmitAdd();
                    break;
                
                case BinaryOpNode.BinaryOp.Sub:
                    EmitSub();
                    break;
                
                case BinaryOpNode.BinaryOp.Mul:
                    EmitMul();
                    break;
                
                case BinaryOpNode.BinaryOp.Div:
                    EmitDiv();
                    break;
            }
        }

        private void Visit(UnaryOpNode unaryOpNode)
        {
            Visit(unaryOpNode.Right);
            
            if (unaryOpNode.Operation == UnaryOpNode.UnaryOp.Minus)
            {
                EmitNeg();
            }
        }

        private void EmitLoad(decimal number)
        {
            _program.Add((byte)OpCode.LOAD);
            
            var ints = decimal.GetBits(number);
            foreach (var i in ints)
            {
                var bytes = BitConverter.GetBytes(i);
                foreach (var b in bytes)
                {
                    _program.Add(b);
                }
            }
        }

        private void EmitNeg()
        {
            _program.Add((byte)OpCode.NEG);
        }

        private void EmitAdd()
        {
            _program.Add((byte)OpCode.ADD);
        }

        private void EmitSub()
        {
            _program.Add((byte)OpCode.SUB);
        }

        private void EmitMul()
        {
            _program.Add((byte)OpCode.MUL);
        }

        private void EmitDiv()
        {
            _program.Add((byte)OpCode.DIV);
        }

        private void EmitStor()
        {
            _program.Add((byte)OpCode.STOR);
        }
    }
}