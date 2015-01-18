using System.Collections.Generic;
using NSqlParser.Statement.Select;

namespace NSqlParser.Statement.Create.Table
{
    public class CreateTable : IStatement
    {

        private Schema.Table table;
        private bool unlogged = false;
        private List<string> createOptionsstrings;
        private List<string> tableOptionsstrings;
        private List<ColumnDefinition> columnDefinitions;
        private List<Index> indexes;
        private Select.Select select;

        public void Accept(IStatementVisitor statementVisitor)
        {
            statementVisitor.Visit(this);
        }

        /**
     * The name of the table to be created
     */
        public Schema.Table getTable()
        {
            return table;
        }

        public void setTable(Schema.Table table)
        {
            this.table = table;
        }

        /**
     * Whether the table is unlogged or not (PostgreSQL 9.1+ feature)
     * @return 
     */
        public bool isUnlogged() { return unlogged; }

        public void setUnlogged(bool unlogged) { this.unlogged = unlogged; }

        /**
     * A list of {@link ColumnDefinition}s of this table.
     */
        public List<ColumnDefinition> getColumnDefinitions()
        {
            return columnDefinitions;
        }

        public void setColumnDefinitions(List<ColumnDefinition> list)
        {
            columnDefinitions = list;
        }

        /**
     * A list of options (as simple strings) of this table definition, as
     * ("TYPE", "=", "MYISAM")
     */
        public List<string> getTableOptionsstrings()
        {
            return tableOptionsstrings;
        }

        public void setTableOptionsstrings(List<string> list)
        {
            tableOptionsstrings = list;
        }

        public List<string> getCreateOptionsstrings()
        {
            return createOptionsstrings;
        }

        public void setCreateOptionsstrings(List<string> createOptionsstrings)
        {
            this.createOptionsstrings = createOptionsstrings;
        }



        /**
     * A list of {@link Index}es (for example "PRIMARY KEY") of this table.<br>
     * Indexes created with column definitions (as in mycol INT PRIMARY KEY) are
     * not inserted into this list.
     */
        public List<Index> getIndexes()
        {
            return indexes;
        }

        public void setIndexes(List<Index> list)
        {
            indexes = list;
        }

        public Select.Select getSelect()
        {
            return select;
        }

        public void setSelect(Select.Select select)
        {
            this.select = select;
        }


        public override string ToString()
        {
            string sql = "";
            string createOps = PlainSelect.getStringList(createOptionsstrings, false, false);

            sql = "CREATE " + (unlogged ? "UNLOGGED " : "") +
                  (!"".Equals(createOps) ? createOps + " " : "") +
                  "TABLE " + table;

            if (select != null)
            {
                sql += " AS " + select.ToString();
            }
            else
            {
                sql += " (";

                sql += PlainSelect.getStringList(columnDefinitions, true, false);
                if (indexes != null && indexes.Count != 0)
                {
                    sql += ", ";
                    sql += PlainSelect.getStringList(indexes);
                }
                sql += ")";
                string options = PlainSelect.getStringList(tableOptionsstrings, false, false);
                if (options != null && options.Length > 0)
                {
                    sql += " " + options;
                }
            }

            return sql;
        }
    }
}
