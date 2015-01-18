/**
 * A "NULL" in a sql statement
 */
namespace NSqlParser.Expression
{
    public class NullValue : IExpression {

	
        public void Accept(IExpressionVisitor expressionVisitor) {
            expressionVisitor.Visit(this);
        }


        public override string ToString()
        {
            return "NULL";
        }
    }
}
