namespace VmTest.Grammar
{
    public class UnaryOpNode : AstNode
    {
        public enum UnaryOp
        {
            Plus,
            Minus
        }

        public AstNode Right { get; }
        public UnaryOp Operation { get; }

        public UnaryOpNode(AstNode right, UnaryOp operation)
        {
            Right = right;
            Operation = operation;
        }
    }
}