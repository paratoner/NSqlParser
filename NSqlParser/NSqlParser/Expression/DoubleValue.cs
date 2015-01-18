/**
 * Every number with a point or a exponential format is a DoubleValue
 */
namespace NSqlParser.Expression
{
    public class DoubleValue : IExpression {

        private double value;
        private string stringValue;

        public DoubleValue(string value) {
            string val = value;
            if (val[0] == '+') {
                val = val.Substring(1);
            }
            this.value = double.Parse(val);
            this.stringValue = val;
        }

	
        public void Accept(IExpressionVisitor expressionVisitor) {
            expressionVisitor.Visit(this);
        }

        public double getValue() {
            return value;
        }

        public void setValue(double d) {
            value = d;
        }


        public override string ToString()
        {
            return stringValue;
        }
    }
}
