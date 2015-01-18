/**
 * Extract value from date/time expression. The name stores the part - name to
 * get from the following date/time expression.
 *
 * @author Paratoner
 */
namespace NSqlParser.Expression
{
    public class ExtractExpression : IExpression {

        private string name;
        private IExpression expression;

	
        public void Accept(IExpressionVisitor expressionVisitor) {
            expressionVisitor.Visit(this);
        }

        public string getName() {
            return name;
        }

        public void setName(string name) {
            this.name = name;
        }

        public IExpression getExpression() {
            return expression;
        }

        public void setExpression(IExpression expression) {
            this.expression = expression;
        }


        public override string ToString()
        {
            return "EXTRACT(" + name + " FROM " + expression + ')';
        }
    }
}
