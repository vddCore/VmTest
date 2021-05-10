namespace VmTest.Grammar
{
    public class BinaryOpNode : AstNode
    {
        public enum BinaryOp
        {
            Add,
            Sub,
            Mul,
            Div
        }
        
        public AstNode Left { get; }
        public AstNode Right { get; }

        public BinaryOp Operation { get; }

        public BinaryOpNode(AstNode left, AstNode right, BinaryOp operation)
        {
            Left = left;
            Right = right;
            Operation = operation;
        }
    }
}