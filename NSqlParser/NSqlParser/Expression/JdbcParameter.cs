/**
 * A '?' in a statement
 */
namespace NSqlParser.Expression
{
    public class JdbcParameter : IExpression
    {

        public void Accept(IExpressionVisitor expressionVisitor)
        {
            expressionVisitor.Visit(this);
        }

        public override string ToString()
        {
            return "?";
        }
    }
}
