namespace NSqlParser.Expression
{
    public interface IExpression {

        void Accept(IExpressionVisitor expressionVisitor);
    }
}
