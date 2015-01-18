/**
 * A Time in the form {t 'hh:mm:ss'}
 */

using System;

namespace NSqlParser.Expression
{
    public class TimeValue : IExpression
    {

        private TimeSpan value;

        public TimeValue(string value)
        {
            this.value = TimeSpan.Parse(value.Substring(1, value.Length - 1));
        }


        public void Accept(IExpressionVisitor expressionVisitor)
        {
            expressionVisitor.Visit(this);
        }

        public TimeSpan getValue()
        {
            return value;
        }

        public void setValue(TimeSpan d)
        {
            value = d;
        }


        public override string ToString()
        {
            return "{t '" + value + "'}";
        }
    }
}
