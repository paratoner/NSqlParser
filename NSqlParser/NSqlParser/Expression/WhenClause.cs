/**
 * A clause of following syntax: WHEN condition THEN expression. Which is part
 * of a CaseExpression.
 *
 * @author Havard Rast Blok
 */
namespace NSqlParser.Expression
{
    public class WhenClause : IExpression {

        private IExpression whenExpression;
        private IExpression thenExpression;

        /*
	 * (non-Javadoc)
	 * 
	 * @see NSqlParser.expression.Expression#Accept(NSqlParser.expression.ExpressionVisitor)
	 */
	
        public void Accept(IExpressionVisitor expressionVisitor) {
            expressionVisitor.Visit(this);
        }

        /**
	 * @return Returns the thenExpression.
	 */
        public IExpression getThenExpression() {
            return thenExpression;
        }

        /**
	 * @param thenExpression The thenExpression to set.
	 */
        public void setThenExpression(IExpression thenExpression) {
            this.thenExpression = thenExpression;
        }

        /**
	 * @return Returns the whenExpression.
	 */
        public IExpression getWhenExpression() {
            return whenExpression;
        }

        /**
	 * @param whenExpression The whenExpression to set.
	 */
        public void setWhenExpression(IExpression whenExpression) {
            this.whenExpression = whenExpression;
        }


        public override string ToString()
        {
            return "WHEN " + whenExpression + " THEN " + thenExpression;
        }
    }
}
