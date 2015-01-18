
namespace NSqlParser.Expression.Operators.Relational
{
    public class MinorThan : OldOracleJoinBinaryExpression {


        public override void Accept(IExpressionVisitor expressionVisitor)
        {
            expressionVisitor.Visit(this);
        }

	
        public override string getStringExpression() {
            return "<";
        }
    }
}
