/**
 * A function as MAX,COUNT...
 */

using NSqlParser.Expression.Operators.Relational;
using NSqlParser.Util;

namespace NSqlParser.Expression
{
    public class Function : IExpression
    {

        private string name;
        private ExpressionList parameters;
        private bool allColumns = false;
        private bool distinct = false;
        private bool isEscaped = false;
        private string attribute;


        public void Accept(IExpressionVisitor expressionVisitor)
        {
            expressionVisitor.Visit(this);
        }

        /**
     * The name of he function, i.e. "MAX"
     *
     * @return the name of he function
     */
        public string getName()
        {
            return name;
        }

        public void setName(string stringn)
        {
            name = stringn;
        }

        /**
     * true if the parameter to the function is "*"
     *
     * @return true if the parameter to the function is "*"
     */
        public bool isAllColumns()
        {
            return allColumns;
        }

        public void setAllColumns(bool b)
        {
            allColumns = b;
        }

        /**
     * true if the function is "distinct"
     *
     * @return true if the function is "distinct"
     */
        public bool isDistinct()
        {
            return distinct;
        }

        public void setDistinct(bool b)
        {
            distinct = b;
        }

        /**
     * The list of parameters of the function (if any, else null) If the
     * parameter is "*", allColumns is set to true
     *
     * @return the list of parameters of the function (if any, else null)
     */
        public ExpressionList getParameters()
        {
            return parameters;
        }

        public void setParameters(ExpressionList list)
        {
            parameters = list;
        }

        /**
     * Return true if it's in the form "{fn function_body() }"
     *
     * @return true if it's java-escaped
     */
        public bool IsEscaped()
        {
            return isEscaped;
        }

        public void setEscaped(bool isEscaped)
        {
            this.isEscaped = isEscaped;
        }

        public string getAttribute()
        {
            return attribute;
        }

        public void setAttribute(string attribute)
        {
            this.attribute = attribute;
        }


        public override string ToString()
        {
            string parms;

            if (parameters != null)
            {
                parms = parameters.ToString();
                if (isDistinct())
                {
                    parms = parms.ReplaceFirst("\\(", "(DISTINCT ");
                }
                else if (isAllColumns())
                {
                    parms = parms.ReplaceFirst("\\(", "(ALL ");
                }
            }
            else if (isAllColumns())
            {
                parms = "(*)";
            }
            else
            {
                parms = "()";
            }

            string ans = name + "" + parms + "";

            if (attribute != null)
            {
                ans += "." + attribute;
            }

            if (isEscaped)
            {
                ans = "{fn " + ans + "}";
            }

            return ans;
        }
    }
}
