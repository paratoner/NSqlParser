using System.Collections.Generic;
using System.Text;
using NSqlParser.Expression.Operators.Relational;
using NSqlParser.Schema;
using NSqlParser.Statement.Select;

namespace NSqlParser.Statement.Insert
{
    public class Insert : IStatement
    {

        private Table table;
        private List<Column> columns;
        private IItemsList itemsList;
        private bool useValues = true;
        private Select.Select select;
        private bool useSelectBrackets = true;

        private bool returningAllColumns = false;

        private List<SelectExpressionItem> returningExpressionList = null;

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
     * Get the columns (found in "INSERT INTO (col1,col2..) [...]" )
     *
     * @return a list of {@link NSqlParser.schema.Column}
     */
        public List<Column> getColumns()
        {
            return columns;
        }

        public void setColumns(List<Column> list)
        {
            columns = list;
        }

        /**
     * Get the values (as VALUES (...) or SELECT)
     *
     * @return the values of the insert
     */
        public IItemsList getItemsList()
        {
            return itemsList;
        }

        public void setItemsList(IItemsList list)
        {
            itemsList = list;
        }

        public bool isUseValues()
        {
            return useValues;
        }

        public void setUseValues(bool useValues)
        {
            this.useValues = useValues;
        }

        public bool isReturningAllColumns()
        {
            return returningAllColumns;
        }

        public void setReturningAllColumns(bool returningAllColumns)
        {
            this.returningAllColumns = returningAllColumns;
        }

        public List<SelectExpressionItem> getReturningExpressionList()
        {
            return returningExpressionList;
        }

        public void setReturningExpressionList(List<SelectExpressionItem> returningExpressionList)
        {
            this.returningExpressionList = returningExpressionList;
        }

        public Select.Select getSelect()
        {
            return select;
        }

        public void setSelect(Select.Select select)
        {
            this.select = select;
        }

        public bool isUseSelectBrackets()
        {
            return useSelectBrackets;
        }

        public void setUseSelectBrackets(bool useSelectBrackets)
        {
            this.useSelectBrackets = useSelectBrackets;
        }

        public override string ToString()
        {
            StringBuilder sql = new StringBuilder();

            sql.Append("INSERT INTO ");
            sql.Append(table).Append(" ");
            if (columns != null)
            {
                sql.Append(PlainSelect.getStringList(columns, true, true)).Append(" ");
            }

            if (useValues)
            {
                sql.Append("VALUES ");
            }

            if (itemsList != null)
            {
                sql.Append(itemsList);
            }

            if (useSelectBrackets)
            {
                sql.Append("(");
            }
            if (select != null)
            {
                sql.Append(select);
            }
            if (useSelectBrackets)
            {
                sql.Append(")");
            }

            if (isReturningAllColumns())
            {
                sql.Append(" RETURNING *");
            }
            else if (getReturningExpressionList() != null)
            {
                sql.Append(" RETURNING ").Append(PlainSelect.getStringList(getReturningExpressionList(), true, false));
            }

            return sql.ToString();
        }
    }
}
