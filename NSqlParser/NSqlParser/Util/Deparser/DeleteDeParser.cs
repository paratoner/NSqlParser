/**
 * A class to de-parse (that is, tranform from NSqlParser hierarchy into a
 * string) a {@link NSqlParser.statement.delete.Delete}
 */

using System.Text;
using NSqlParser.Expression;
using NSqlParser.Statement.Delete;

namespace NSqlParser.Util.Deparser
{
    public class DeleteDeParser
    {

        private StringBuilder buffer;
        private IExpressionVisitor expressionVisitor;

        /**
     * @param expressionVisitor a {@link ExpressionVisitor} to de-parse
     * expressions. It has to share the same<br>
     * StringBuilder (buffer parameter) as this object in order to work
     * @param buffer the buffer that will be filled with the select
     */
        public DeleteDeParser(IExpressionVisitor expressionVisitor, StringBuilder buffer)
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

        public void deParse(Delete delete)
        {
            buffer.Append("DELETE FROM ").Append(delete.getTable().GetFullyQualifiedName());
            if (delete.getWhere() != null)
            {
                buffer.Append(" WHERE ");
                delete.getWhere().Accept(expressionVisitor);
            }

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
