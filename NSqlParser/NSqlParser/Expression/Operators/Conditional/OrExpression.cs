namespace NSqlParser.Expression.Operators.Conditional
{
    public class OrExpression : BinaryExpression
    {

        public OrExpression(IExpression leftExpression, IExpression rightExpression)
        {
            setLeftExpression(leftExpression);
            setRightExpression(rightExpression);
        }


        public override void Accept(IExpressionVisitor expressionVisitor)
        {
            expressionVisitor.Visit(this);
        }


        public override string getStringExpression()
        {
            return "OR";
        }
    }
}
