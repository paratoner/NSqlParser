using System.Text;
using NSqlParser.Expression;
using NSqlParser.Schema;
using NSqlParser.Statement.Select;
using NSqlParser.Statement.Update;
/**
 * A class to de-parse (that is, tranform from NSqlParser hierarchy into a
 * string) an {@link NSqlParser.statement.update.Update}
 */

namespace NSqlParser.Util.Deparser
{
    public class UpdateDeParser
    {

        private StringBuilder buffer;
        private IExpressionVisitor expressionVisitor;
        private ISelectVisitor selectVisitor;

        /**
     * @param expressionVisitor a {@link ExpressionVisitor} to de-parse
     * expressions. It has to share the same<br>
     * StringBuilder (buffer parameter) as this object in order to work
     * @param selectVisitor a {@link SelectVisitor} to de-parse
     * {@link NSqlParser.statement.select.Select}s. It has to share the
     * same<br>
     * StringBuilder (buffer parameter) as this object in order to work
     * @param buffer the buffer that will be filled with the select
     */
        public UpdateDeParser(IExpressionVisitor expressionVisitor, ISelectVisitor selectVisitor, StringBuilder buffer)
        {
            this.buffer = buffer;
            this.expressionVisitor = expressionVisitor;
            this.selectVisitor = selectVisitor;
        }

        public StringBuilder getBuffer()
        {
            return buffer;
        }

        public void setBuffer(StringBuilder buffer)
        {
            this.buffer = buffer;
        }

        public void deParse(Update update)
        {
            buffer.Append("UPDATE ").Append(PlainSelect.getStringList(update.getTables(), true, false)).Append(" SET ");

            if (!update.isUseSelect())
            {
                for (int i = 0; i < update.getColumns().Count; i++)
                {
                    Column column = update.getColumns()[i];
                    buffer.Append(column.GetFullyQualifiedName()).Append(" = ");

                    IExpression expression = update.getExpressions()[i];
                    expression.Accept(expressionVisitor);
                    if (i < update.getColumns().Count - 1)
                    {
                        buffer.Append(", ");
                    }
                }
            }
            else
            {
                if (update.isUseColumnsBrackets())
                {
                    buffer.Append("(");
                }
                for (int i = 0; i < update.getColumns().Count; i++)
                {
                    if (i != 0)
                    {
                        buffer.Append(", ");
                    }
                    Column column = update.getColumns()[i];
                    buffer.Append(column.GetFullyQualifiedName());
                }
                if (update.isUseColumnsBrackets())
                {
                    buffer.Append(")");
                }
                buffer.Append(" = ");
                buffer.Append("(");
                Select select = update.getSelect();
                select.getSelectBody().Accept(selectVisitor);
                buffer.Append(")");
            }

            if (update.getFromItem() != null)
            {
                buffer.Append(" FROM ").Append(update.getFromItem());
                if (update.getJoins() != null)
                {
                    foreach (Join join in update.getJoins())
                    {
                        if (join.isSimple())
                        {
                            buffer.Append(", ").Append(join);
                        }
                        else
                        {
                            buffer.Append(" ").Append(join);
                        }
                    }
                }
            }

            if (update.getWhere() != null)
            {
                buffer.Append(" WHERE ");
                update.getWhere().Accept(expressionVisitor);
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
