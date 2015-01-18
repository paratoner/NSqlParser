using System;
using System.Linq;
using System.Text;
using NSqlParser.Expression;
using NSqlParser.Expression.Operators.Relational;
using NSqlParser.Schema;
using NSqlParser.Statement.Replace;
using NSqlParser.Statement.Select;
/**
 * A class to de-parse (that is, tranform from JSqlParser hierarchy into a
 * string) a {@link net.sf.jsqlparser.statement.replace.Replace}
 */

namespace NSqlParser.Util.Deparser
{
    public class ReplaceDeParser : IItemsListVisitor
    {

        private StringBuilder buffer;
        private IExpressionVisitor expressionVisitor;
        private ISelectVisitor selectVisitor;

        public ReplaceDeParser()
        {
        }

        /**
     * @param expressionVisitor a {@link ExpressionVisitor} to de-parse
     * expressions. It has to share the same<br>
     * StringBuilder (buffer parameter) as this object in order to work
     * @param selectVisitor a {@link SelectVisitor} to de-parse
     * {@link net.sf.jsqlparser.statement.select.Select}s. It has to share the
     * same<br>
     * StringBuilder (buffer parameter) as this object in order to work
     * @param buffer the buffer that will be filled with the select
     */
        public ReplaceDeParser(IExpressionVisitor expressionVisitor, ISelectVisitor selectVisitor, StringBuilder buffer)
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

        public void deParse(Replace replace)
        {
            buffer.Append("REPLACE ").Append(replace.getTable().GetFullyQualifiedName());
            if (replace.getItemsList() != null)
            {
                if (replace.getColumns() != null)
                {
                    buffer.Append(" (");
                    for (int i = 0; i < replace.getColumns().Count; i++)
                    {
                        Column column = replace.getColumns()[i];
                        buffer.Append(column.GetFullyQualifiedName());
                        if (i < replace.getColumns().Count - 1)
                        {
                            buffer.Append(", ");
                        }
                    }
                    buffer.Append(") ");
                }
                else
                {
                    buffer.Append(" ");
                }

            }
            else
            {
                buffer.Append(" SET ");
                for (int i = 0; i < replace.getColumns().Count; i++)
                {
                    Column column = replace.getColumns()[i];
                    buffer.Append(column.GetFullyQualifiedName()).Append("=");

                    IExpression expression = replace.getExpressions()[i];
                    expression.Accept(expressionVisitor);
                    if (i < replace.getColumns().Count - 1)
                    {
                        buffer.Append(", ");
                    }

                }
            }

            if (replace.getItemsList() != null)
            {
                // REPLACE mytab SELECT * FROM mytab2
                // or VALUES ('as', ?, 565)

                if (replace.isUseValues())
                {
                    buffer.Append(" VALUES");
                }

                buffer.Append(replace.getItemsList());
            }
        }

        public void Visit(ExpressionList expressionList)
        {
            buffer.Append(" VALUES (");
            foreach (IExpression expression in expressionList.getExpressions())
            {
                expression.Accept(expressionVisitor);
                if (!(expression == expressionList.getExpressions().Last()))
                {
                    buffer.Append(", ");
                }
            }
            buffer.Append(")");
        }

        public void Visit(SubSelect subSelect)
        {
            subSelect.getSelectBody().Accept(selectVisitor);
        }

        public IExpressionVisitor getExpressionVisitor()
        {
            return expressionVisitor;
        }

        public ISelectVisitor getSelectVisitor()
        {
            return selectVisitor;
        }

        public void setExpressionVisitor(IExpressionVisitor visitor)
        {
            expressionVisitor = visitor;
        }

        public void setSelectVisitor(ISelectVisitor visitor)
        {
            selectVisitor = visitor;
        }

        public void Visit(MultiExpressionList multiExprList)
        {
            throw new NotSupportedException("Not supported yet.");
        }
    }
}
