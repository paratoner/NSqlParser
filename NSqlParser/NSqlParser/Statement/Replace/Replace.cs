using System.Collections.Generic;
using System.Text;
using NSqlParser.Expression;
using NSqlParser.Expression.Operators.Relational;
using NSqlParser.Schema;
using NSqlParser.Statement.Select;

namespace NSqlParser.Statement.Replace
{
    public class Replace : IStatement
    {

        private Table table;
        private List<Column> columns;
        private IItemsList itemsList;
        private List<IExpression> expressions;
        private bool useValues = true;

        public void Accept(IStatementVisitor statementVisitor)
        {
            statementVisitor.Visit(this);
        }

        public Table getTable()
        {
            return table;
        }

        public void setTable(Table name)
        {
            table = name;
        }

        /**
     * A list of {@link NSqlParser.schema.Column}s either from a "REPLACE
     * mytab (col1, col2) [...]" or a "REPLACE mytab SET col1=exp1, col2=exp2".
     *
     * @return a list of {@link NSqlParser.schema.Column}s
     */
        public List<Column> getColumns()
        {
            return columns;
        }

        /**
     * An {@link ItemsList} (either from a "REPLACE mytab VALUES (exp1,exp2)" or
     * a "REPLACE mytab SELECT * FROM mytab2") it is null in case of a "REPLACE
     * mytab SET col1=exp1, col2=exp2"
     */
        public IItemsList getItemsList()
        {
            return itemsList;
        }

        public void setColumns(List<Column> list)
        {
            columns = list;
        }

        public void setItemsList(IItemsList list)
        {
            itemsList = list;
        }

        /**
     * A list of {@link NSqlParser.expression.Expression}s (from a
     * "REPLACE mytab SET col1=exp1, col2=exp2"). <br>
     * it is null in case of a "REPLACE mytab (col1, col2) [...]"
     */
        public List<IExpression> getExpressions()
        {
            return expressions;
        }

        public void setExpressions(List<IExpression> list)
        {
            expressions = list;
        }

        public bool isUseValues()
        {
            return useValues;
        }

        public void setUseValues(bool useValues)
        {
            this.useValues = useValues;
        }

        public override string ToString()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("REPLACE ").Append(table);

            if (expressions != null && columns != null)
            {
                // the SET col1=exp1, col2=exp2 case
                sql.Append(" SET ");
                // each element from expressions match up with a column from columns.
                for (int i = 0, s = columns.Count; i < s; i++)
                {
                    sql.Append(columns[i]).Append("=").Append(expressions[i]);
                    sql.Append((i < s - 1) ? ", " : "");
                }
            }
            else if (columns != null)
            {
                // the REPLACE mytab (col1, col2) [...] case
                sql.Append(" ").Append(PlainSelect.getStringList(columns, true, true));
            }

            if (itemsList != null)
            {
                // REPLACE mytab SELECT * FROM mytab2
                // or VALUES ('as', ?, 565)

                if (useValues)
                {
                    sql.Append(" VALUES");
                }

                sql.Append(" ").Append(itemsList);
            }

            return sql.ToString();
        }
    }
}
