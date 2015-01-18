namespace NSqlParser.Expression.Operators.Relational
{
    public class IsNullExpression : IExpression {

        private IExpression leftExpression;
        private bool not = false;

        public IExpression getLeftExpression() {
            return leftExpression;
        }

        public bool isNot() {
            return not;
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
            return leftExpression + " IS " + ((not) ? "NOT " : "") + "NULL";
        }
    }
}
