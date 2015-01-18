/**
 * It represents an expression like "(" expression ")"
 */
namespace NSqlParser.Expression
{
    public class Parenthesis : IExpression {

        private IExpression expression;
        private bool not = false;

        public Parenthesis() {
        }

        public Parenthesis(IExpression expression) {
            setExpression(expression);
        }

        public IExpression getExpression() {
            return expression;
        }

        public void setExpression(IExpression expression) {
            this.expression = expression;
        }

	
        public void Accept(IExpressionVisitor expressionVisitor) {
            expressionVisitor.Visit(this);
        }

        public void setNot() {
            not = true;
        }

        public bool isNot() {
            return not;
        }


        public override string ToString()
        {
            return (not ? "NOT " : "") + "(" + expression + ")";
        }
    }
}
