namespace NSqlParser.Expression.Operators.Relational
{
    public class ExistsExpression : IExpression
    {

        private IExpression rightExpression;
        private bool not = false;

        public IExpression getRightExpression() {
            return rightExpression;
        }

        public void setRightExpression(IExpression expression) {
            rightExpression = expression;
        }

        public bool isNot() {
            return not;
        }

        public void setNot(bool b) {
            not = b;
        }

	
        public void Accept(IExpressionVisitor expressionVisitor) {
            expressionVisitor.Visit(this);
        }

        public string getStringExpression() {
            return ((not) ? "NOT " : "") + "EXISTS";
        }


        public override string ToString()
        {
            return getStringExpression() + " " + rightExpression.ToString();
        }
    }
}
