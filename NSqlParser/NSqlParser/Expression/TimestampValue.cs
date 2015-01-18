/**
 * A Timestamp in the form {ts 'yyyy-mm-dd hh:mm:ss.f . . .'}
 */

using System;

namespace NSqlParser.Expression
{
    public class TimestampValue : IExpression
    {

        private DateTime value;

        public TimestampValue(string value)
        {
            this.value = DateTime.Parse(value.Substring(1, value.Length - 1));
        }


        public void Accept(IExpressionVisitor expressionVisitor)
        {
            expressionVisitor.Visit(this);
        }

        public DateTime getValue()
        {
            return value;
        }

        public void setValue(DateTime d)
        {
            value = d;
        }


        public override string ToString()
        {
            return "{ts '" + value + "'}";
        }
    }
}
