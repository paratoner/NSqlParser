
namespace NSqlParser.Expression.Operators.Conditional
{
    public class AndExpression : BinaryExpression {

        public AndExpression(IExpression leftExpression, IExpression rightExpression) {
            base.setLeftExpression(leftExpression);
            base.setRightExpression(rightExpression);
        }


        public override void Accept(IExpressionVisitor expressionVisitor)
        {
            expressionVisitor.Visit(this);
        }

	
        public override string getStringExpression() {
            return "AND";
        }
    }
}
