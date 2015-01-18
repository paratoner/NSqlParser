using System.Linq;
using System.Text;

namespace NSqlParser.Statement.Create.Index
{
    public class CreateIndex : IStatement
    {

        private Schema.Table table;
        private Table.Index index;

        public void Accept(IStatementVisitor statementVisitor)
        {
            statementVisitor.Visit(this);
        }

        /**
     * The index to be created
     */
        public Table.Index getIndex()
        {
            return index;
        }

        public void setIndex(Table.Index index)
        {
            this.index = index;
        }

        /**
     * The table on which the index is to be created
     */
        public Schema.Table getTable()
        {
            return table;
        }

        public void setTable(Schema.Table table)
        {
            this.table = table;
        }


        public override string ToString()
        {
            StringBuilder buffer = new StringBuilder();

            buffer.Append("CREATE ");

            if (index.getType() != null)
            {
                buffer.Append(index.getType());
                buffer.Append(" ");
            }

            buffer.Append("INDEX ");
            buffer.Append(index.getName());
            buffer.Append(" ON ");
            buffer.Append(table.GetFullyQualifiedName());

            if (index.getColumnsNames() != null)
            {
                buffer.Append(" (");
                foreach (string columnName in index.getColumnsNames())
                {
                    buffer.Append(columnName);

                    if (!(columnName == index.getColumnsNames().Last()))
                    {
                        buffer.Append(", ");
                    }
                }

                buffer.Append(")");
            }

            return buffer.ToString();
        }

    }
}
