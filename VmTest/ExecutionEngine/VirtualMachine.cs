using System;
using System.Collections.Generic;

namespace VmTest.ExecutionEngine
{
    public class VirtualMachine
    {
        public decimal R { get; private set; }

        public byte[] Program { get; }

        public Stack<decimal> EvaluationStack { get; } = new();
        public int PC { get; private set; }

        public VirtualMachine(byte[] program)
        {
            Program = program;
        }

        public void Run()
        {
            decimal a;
            decimal b;

            while (PC < Program.Length)
            {
                var instr = Program[PC++];

                switch ((OpCode)instr)
                {
                    case OpCode.LOAD:
                        EvaluationStack.Push(ReadImmediateDecimal());
                        break;

                    case OpCode.STOR:
                        AssumeStackAtLeast(1);
                        R = EvaluationStack.Pop();
                        break;

                    case OpCode.ADD:
                        AssumeStackAtLeast(2);
                        a = EvaluationStack.Pop();
                        b = EvaluationStack.Pop();

                        EvaluationStack.Push(a + b);
                        break;

                    case OpCode.SUB:
                        AssumeStackAtLeast(2);
                        a = EvaluationStack.Pop();
                        b = EvaluationStack.Pop();

                        EvaluationStack.Push(a - b);
                        break;

                    case OpCode.DIV:
                        AssumeStackAtLeast(2);

                        a = EvaluationStack.Pop();
                        b = EvaluationStack.Pop();

                        if (b == 0)
                            throw new Exception("Division by zero.");

                        EvaluationStack.Push(a / b);
                        break;

                    case OpCode.MUL:
                        AssumeStackAtLeast(2);

                        a = EvaluationStack.Pop();
                        b = EvaluationStack.Pop();

                        EvaluationStack.Push(a * b);
                        break;

                    case OpCode.NEG:
                        AssumeStackAtLeast(1);

                        EvaluationStack.Push(
                            -EvaluationStack.Pop()
                        );
                        break;

                    default: throw new Exception("Invalid opcode.");
                }
            }
        }

        private void AssumeStackAtLeast(int size)
        {
            if (EvaluationStack.Count < size)
                throw new Exception("Stack underflow.");
        }

        private decimal ReadImmediateDecimal()
        {
            var arr = new[]
            {
                ReadImmediateInteger(),
                ReadImmediateInteger(),
                ReadImmediateInteger(),
                ReadImmediateInteger()
            };

            return new decimal(arr);
        }

        private int ReadImmediateInteger()
        {
            var b1 = Program[PC++];
            var b2 = Program[PC++];
            var b3 = Program[PC++];
            var b4 = Program[PC++];

            var ret = 0;

            ret |= b4 << 24;
            ret |= b3 << 16;
            ret |= b2 << 8;
            ret |= b1;

            return ret;
        }
    }
}