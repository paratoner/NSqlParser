/**
 * A string as in 'example_string'
 */

using System.Text;
using NSqlParser.Util;

namespace NSqlParser.Expression
{
    public class StringValue : IExpression
    {

        private string value = "";

        public StringValue(string escapedValue)
        {
            // romoving "'" at the start and at the end
            value = escapedValue.Substring(1, escapedValue.Length - 1);
        }

        public string getValue()
        {
            return value;
        }

        public string getNotExcapedValue()
        {
            StringBuilder buffer = new StringBuilder(value);
            int index = 0;
            int deletesNum = 0;
            while ((index = value.IndexOf("''", index)) != -1)
            {
                buffer.DeleteCharAt(index - deletesNum);
                index += 2;
                deletesNum++;
            }
            return buffer.ToString();
        }

        public void setValue(string stringv)
        {
            value = stringv;
        }


        public void Accept(IExpressionVisitor expressionVisitor)
        {
            expressionVisitor.Visit(this);
        }


        public override string ToString()
        {
            return "'" + value + "'";
        }
    }
}
