/**
 * A "BETWEEN" expr1 expr2 statement
 */
namespace NSqlParser.Expression.Operators.Relational
{
    public class Between : IExpression
    {

        private IExpression leftExpression;
        private bool not = false;
        private IExpression betweenExpressionStart;
        private IExpression betweenExpressionEnd;

        public IExpression getBetweenExpressionEnd() {
            return betweenExpressionEnd;
        }

        public IExpression getBetweenExpressionStart() {
            return betweenExpressionStart;
        }

        public IExpression getLeftExpression() {
            return leftExpression;
        }

        public bool isNot() {
            return not;
        }

        public void setBetweenExpressionEnd(IExpression expression) {
            betweenExpressionEnd = expression;
        }

        public void setBetweenExpressionStart(IExpression expression) {
            betweenExpressionStart = expression;
        }

        public void setLeftExpression(IExpression expression) {
            leftExpression = expression;
        }

        public void setNot(bool b) {
            not = b;
        }

	
        public void Accept(IExpressionVisitor expressionVisitor) {
            expressionVisitor.Visit(this);
        }


        public override string ToString()
        {
            return leftExpression + " " + (not ? "NOT " : "") + "BETWEEN " + betweenExpressionStart + " AND "
                   + betweenExpressionEnd;
        }
    }
}
