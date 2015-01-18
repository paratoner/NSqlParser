/**
 * The update statement.
 */

using System.Collections.Generic;
using System.Text;
using NSqlParser.Expression;
using NSqlParser.Schema;
using NSqlParser.Statement.Select;

namespace NSqlParser.Statement.Update
{
    public class Update : IStatement
    {

        private List<Table> tables;
        private IExpression where;
        private List<Column> columns;
        private List<IExpression> expressions;
        private IFromItem fromItem;
        private List<Join> joins;
        private Select.Select select;
        private bool useColumnsBrackets = true;
        private bool useSelect = false;

        public void Accept(IStatementVisitor statementVisitor)
        {
            statementVisitor.Visit(this);
        }

        public List<Table> getTables()
        {
            return tables;
        }

        public IExpression getWhere()
        {
            return where;
        }

        public void setTables(List<Table> list)
        {
            tables = list;
        }

        public void setWhere(IExpression expression)
        {
            where = expression;
        }

        /**
     * The {@link NSqlParser.schema.Column}s in this update (as col1 and
     * col2 in UPDATE col1='a', col2='b')
     *
     * @return a list of {@link NSqlParser.schema.Column}s
     */
        public List<Column> getColumns()
        {
            return columns;
        }

        /**
     * The {@link Expression}s in this update (as 'a' and 'b' in UPDATE
     * col1='a', col2='b')
     *
     * @return a list of {@link Expression}s
     */
        public List<IExpression> getExpressions()
        {
            return expressions;
        }

        public void setColumns(List<Column> list)
        {
            columns = list;
        }

        public void setExpressions(List<IExpression> list)
        {
            expressions = list;
        }

        public IFromItem getFromItem()
        {
            return fromItem;
        }

        public void setFromItem(IFromItem fromItem)
        {
            this.fromItem = fromItem;
        }

        public List<Join> getJoins()
        {
            return joins;
        }

        public void setJoins(List<Join> joins)
        {
            this.joins = joins;
        }

        public Select.Select getSelect()
        {
            return select;
        }

        public void setSelect(Select.Select select)
        {
            this.select = select;
        }

        public bool isUseColumnsBrackets()
        {
            return useColumnsBrackets;
        }

        public void setUseColumnsBrackets(bool useColumnsBrackets)
        {
            this.useColumnsBrackets = useColumnsBrackets;
        }

        public bool isUseSelect()
        {
            return useSelect;
        }

        public void setUseSelect(bool useSelect)
        {
            this.useSelect = useSelect;
        }

        public override string ToString()
        {
            StringBuilder b = new StringBuilder("UPDATE ");
            b.Append(PlainSelect.getStringList(getTables(), true, false)).Append(" SET ");

            if (!useSelect)
            {
                for (int i = 0; i < getColumns().Count; i++)
                {
                    if (i != 0)
                    {
                        b.Append(", ");
                    }
                    b.Append(columns[i]).Append(" = ");
                    b.Append(expressions[i]);
                }
            }
            else
            {
                if (useColumnsBrackets)
                {
                    b.Append("(");
                }
                for (int i = 0; i < getColumns().Count; i++)
                {
                    if (i != 0)
                    {
                        b.Append(", ");
                    }
                    b.Append(columns[i]);
                }
                if (useColumnsBrackets)
                {
                    b.Append(")");
                }
                b.Append(" = ");
                b.Append("(").Append(select).Append(")");
            }

            if (fromItem != null)
            {
                b.Append(" FROM ").Append(fromItem);
                if (joins != null)
                {
                    foreach (var join in joins)
                    {
                        if (join.isSimple())
                        {
                            b.Append(", ").Append(join);
                        }
                        else
                        {
                            b.Append(" ").Append(join);
                        }
                    }
                }
            }

            if (where != null)
            {
                b.Append(" WHERE ");
                b.Append(where);
            }
            return b.ToString();
        }
    }
}
