using System.Text;
using NSqlParser.Expression;
using NSqlParser.Statement.Execute;
using NSqlParser.Statement.Select;

namespace NSqlParser.Util.Deparser
{
    public class ExecuteDeParser
    {

        private StringBuilder buffer;
        private IExpressionVisitor expressionVisitor;

        /**
     * @param expressionVisitor a {@link ExpressionVisitor} to de-parse
     * expressions. It has to share the same<br>
     * StringBuilder (buffer parameter) as this object in order to work
     * @param buffer the buffer that will be filled with the select
     */
        public ExecuteDeParser(IExpressionVisitor expressionVisitor, StringBuilder buffer)
        {
            this.buffer = buffer;
            this.expressionVisitor = expressionVisitor;
        }

        public StringBuilder getBuffer()
        {
            return buffer;
        }

        public void setBuffer(StringBuilder buffer)
        {
            this.buffer = buffer;
        }

        public void deParse(Execute execute)
        {
            buffer.Append("EXECUTE ").Append(execute.getName());
            buffer.Append(" ").Append(PlainSelect.getStringList(execute.getExprList().getExpressions(), true, false));
        }

        public IExpressionVisitor getExpressionVisitor()
        {
            return expressionVisitor;
        }

        public void setExpressionVisitor(IExpressionVisitor visitor)
        {
            expressionVisitor = visitor;
        }
    }
}
