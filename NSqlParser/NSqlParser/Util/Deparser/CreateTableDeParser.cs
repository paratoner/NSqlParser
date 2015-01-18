/**
 * A class to de-parse (that is, tranform from NSqlParser hierarchy into a
 * string) a {@link NSqlParser.statement.create.table.CreateTable}
 */

using System.Linq;
using System.Text;
using NSqlParser.Statement.Create.Table;
using NSqlParser.Statement.Select;

namespace NSqlParser.Util.Deparser
{
    public class CreateTableDeParser
    {

        private StringBuilder buffer;

        /**
     * @param buffer the buffer that will be filled with the select
     */
        public CreateTableDeParser(StringBuilder buffer)
        {
            this.buffer = buffer;
        }

        public void deParse(CreateTable createTable)
        {
            buffer.Append("CREATE ");
            if (createTable.isUnlogged())
            {
                buffer.Append("UNLOGGED ");
            }
            string paramss = PlainSelect.getStringList(createTable.getCreateOptionsstrings(), false, false);
            if (!"".Equals(paramss))
            {
                buffer.Append(paramss).Append(' ');
            }

            buffer.Append("TABLE ").Append(createTable.getTable().GetFullyQualifiedName());
            if (createTable.getSelect() != null)
            {
                buffer.Append(" AS ").Append(createTable.getSelect().ToString());
            }
            else
            {
                if (createTable.getColumnDefinitions() != null)
                {
                    buffer.Append(" (");
                    foreach (ColumnDefinition columnDefinition in createTable.getColumnDefinitions())
                    {
                        buffer.Append(columnDefinition.getColumnName());
                        buffer.Append(" ");
                        buffer.Append(columnDefinition.getColDataType().ToString());
                        if (columnDefinition.getColumnSpecstrings() != null)
                        {
                            foreach (string s in columnDefinition.getColumnSpecstrings())
                            {
                                buffer.Append(" ");
                                buffer.Append(s);
                            }
                        }

                        if (!(columnDefinition == createTable.getColumnDefinitions().Last()))
                        {
                            buffer.Append(", ");
                        }
                    }

                    if (createTable.getIndexes() != null)
                    {
                        foreach (var index in createTable.getIndexes())
                        {
                            buffer.Append(", ");
                            buffer.Append(index.ToString());
                        }
                    }

                    buffer.Append(")");
                }
            }

            paramss = PlainSelect.getStringList(createTable.getTableOptionsstrings(), false, false);
            if (!"".Equals(paramss))
            {
                buffer.Append(' ').Append(paramss);
            }
        }

        public StringBuilder getBuffer()
        {
            return buffer;
        }

        public void setBuffer(StringBuilder buffer)
        {
            this.buffer = buffer;
        }
    }
}
