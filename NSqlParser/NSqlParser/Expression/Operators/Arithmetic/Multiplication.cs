namespace NSqlParser.Expression.Operators.Arithmetic
{
    public class Multiplication : BinaryExpression
    {


        public override void Accept(IExpressionVisitor expressionVisitor)
        {
            expressionVisitor.Visit(this);
        }


        public override string getStringExpression()
        {
            return "*";
        }
    }
}
