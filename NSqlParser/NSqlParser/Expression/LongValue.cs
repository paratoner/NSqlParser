/**
 * Every number without a point or an exponential format is a LongValue
 */
namespace NSqlParser.Expression
{
    public class LongValue : IExpression {

        private long value;
        private string stringValue;

        public LongValue(string value) {
            string val = value;
            if (val[0] == '+') {
                val = val.Substring(1);
            }
            this.value = long.Parse(val);
            this.stringValue = val;
        }
	
        public LongValue(long value) {
            this.value=value;
            stringValue = value.ToString();
        }

	
        public void Accept(IExpressionVisitor expressionVisitor) {
            expressionVisitor.Visit(this);
        }

        public long getValue() {
            return value;
        }

        public void setValue(long d) {
            value = d;
        }

        public string getStringValue() {
            return stringValue;
        }

        public void setStringValue(string stringv) {
            stringValue = stringv;
        }


        public override string ToString()
        {
            return getStringValue();
        }
    }
}
