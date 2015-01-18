using System.Collections.Generic;
using System.Text;
using NSqlParser.Statement.Select;

namespace NSqlParser.Statement.Create.View
{
    public class CreateView : IStatement
    {

        private Schema.Table view;
        private ISelectBody selectBody;
        private bool orReplace = false;
        private List<string> columnNames = null;
        private bool materialized = false;

        public void Accept(IStatementVisitor statementVisitor)
        {
            statementVisitor.Visit(this);
        }

        /**
     * In the syntax tree, a view looks and acts just like a Table.
     *
     * @return The name of the view to be created.
     */
        public Schema.Table getView()
        {
            return view;
        }

        public void setView(Schema.Table view)
        {
            this.view = view;
        }

        /**
     * @return was "OR REPLACE" specified?
     */
        public bool isOrReplace()
        {
            return orReplace;
        }

        /**
     * @param orReplace was "OR REPLACE" specified?
     */
        public void setOrReplace(bool orReplace)
        {
            this.orReplace = orReplace;
        }

        /**
     * @return the SelectBody
     */
        public ISelectBody getSelectBody()
        {
            return selectBody;
        }

        public void setSelectBody(ISelectBody selectBody)
        {
            this.selectBody = selectBody;
        }

        public List<string> getColumnNames()
        {
            return columnNames;
        }

        public void setColumnNames(List<string> columnNames)
        {
            this.columnNames = columnNames;
        }

        public bool isMaterialized()
        {
            return materialized;
        }

        public void setMaterialized(bool materialized)
        {
            this.materialized = materialized;
        }

        public override string ToString()
        {
            StringBuilder sql = new StringBuilder("CREATE ");
            if (isOrReplace())
            {
                sql.Append("OR REPLACE ");
            }
            if (isMaterialized())
            {
                sql.Append("MATERIALIZED ");
            }
            sql.Append("VIEW ");
            sql.Append(view);
            if (columnNames != null)
            {
                sql.Append(PlainSelect.getStringList(columnNames, true, true));
            }
            sql.Append(" AS ").Append(selectBody);
            return sql.ToString();
        }
    }
}
