/**
 *
 * @author Paratoner
 */
namespace NSqlParser.Expression
{
    public class IntervalExpression : IExpression {
        private string parameter = null;

        public string getParameter() {
            return parameter;
        }

        public void setParameter(string parameter) {
            this.parameter = parameter;
        }


        public override string ToString()
        {
            return "INTERVAL " + parameter;
        }

	
        public void Accept(IExpressionVisitor expressionVisitor) {
            expressionVisitor.Visit(this);
        }
    }
}
