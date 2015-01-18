using System.Linq;
using System.Text;
using NSqlParser.Expression;
using NSqlParser.Expression.Operators.Relational;
using NSqlParser.Schema;
using NSqlParser.Statement.Insert;
using NSqlParser.Statement.Select;
/**
 * A class to de-parse (that is, tranform from JSqlParser hierarchy into a
 * string) an {@link net.sf.jsqlparser.statement.insert.Insert}
 */

namespace NSqlParser.Util.Deparser
{
    public class InsertDeParser : IItemsListVisitor
    {

        private StringBuilder buffer;
        private IExpressionVisitor expressionVisitor;
        private ISelectVisitor selectVisitor;

        public InsertDeParser()
        {
        }

        /**
     * @param expressionVisitor a {@link ExpressionVisitor} to de-parse
     * {@link net.sf.jsqlparser.expression.Expression}s. It has to share the
     * same<br>
     * StringBuilder (buffer parameter) as this object in order to work
     * @param selectVisitor a {@link SelectVisitor} to de-parse
     * {@link net.sf.jsqlparser.statement.select.Select}s. It has to share the
     * same<br>
     * StringBuilder (buffer parameter) as this object in order to work
     * @param buffer the buffer that will be filled with the insert
     */
        public InsertDeParser(IExpressionVisitor expressionVisitor, ISelectVisitor selectVisitor, StringBuilder buffer)
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

        public void deParse(Insert insert)
        {
            buffer.Append("INSERT INTO ");
            buffer.Append(insert.getTable().GetFullyQualifiedName());
            if (insert.getColumns() != null)
            {
                buffer.Append(" (");
                foreach (Column column in insert.getColumns())
                {
                    buffer.Append(column.getColumnName());
                    if (!(column == insert.getColumns().Last()))
                    {
                        buffer.Append(", ");
                    }
                }
                buffer.Append(")");
            }

            if (insert.getItemsList() != null)
            {
                insert.getItemsList().Accept(this);
            }

            if (insert.getSelect() != null)
            {
                buffer.Append(" ");
                if (insert.isUseSelectBrackets())
                {
                    buffer.Append("(");
                }
                if (insert.getSelect().getWithItemsList() != null)
                {
                    buffer.Append("WITH ");
                    foreach (WithItem with in insert.getSelect().getWithItemsList())
                    {
                        with.Accept(selectVisitor);
                    }
                    buffer.Append(" ");
                }
                insert.getSelect().getSelectBody().Accept(selectVisitor);
                if (insert.isUseSelectBrackets())
                {
                    buffer.Append(")");
                }
            }

            if (insert.isReturningAllColumns())
            {
                buffer.Append(" RETURNING *");
            }
            else if (insert.getReturningExpressionList() != null)
            {
                buffer.Append(" RETURNING ");
                foreach (SelectExpressionItem item in insert.getReturningExpressionList())
                {
                    buffer.Append(item.ToString());
                    if (!(item == insert.getReturningExpressionList().Last()))
                    {
                        buffer.Append(", ");
                    }
                }
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

        public void Visit(MultiExpressionList multiExprList)
        {
            buffer.Append(" VALUES ");
            foreach (ExpressionList it in multiExprList.getExprList())
            {
                buffer.Append("(");
                foreach (IExpression expression in it.getExpressions())
                {
                    expression.Accept(expressionVisitor);
                    if (!(expression == it.getExpressions().Last()))
                    {
                        buffer.Append(", ");
                    }
                }
                buffer.Append(")");
                if (!(it == multiExprList.getExprList().Last()))
                {
                    buffer.Append(", ");
                }
            }

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
    }
}
