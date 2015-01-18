/**
 * A Date in the form {d 'yyyy-mm-dd'}
 */

using System;

namespace NSqlParser.Expression
{
    public class DateValue : IExpression
    {

        private DateTime value;

        public DateValue(string value)
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
    }
}
