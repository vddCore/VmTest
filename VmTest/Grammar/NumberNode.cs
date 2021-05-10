namespace VmTest.Grammar
{
    public class NumberNode : AstNode
    {
        public decimal Value { get; }

        public NumberNode(decimal value)
        {
            Value = value;
        }
    }
}