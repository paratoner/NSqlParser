/**
 * A class to de-parse (that is, tranform from JSqlParser hierarchy into a string)
 * a {@link net.sf.jsqlparser.statement.create.index.CreateIndex}
 *
 * @author Paratoner
 */

using System.Linq;
using System.Text;
using NSqlParser.Statement.Create.Index;
using NSqlParser.Statement.Create.Table;

namespace NSqlParser.Util.Deparser
{
    public class CreateIndexDeParser
    {

        private StringBuilder buffer;

        /**
     * @param buffer the buffer that will be filled with the create
     */
        public CreateIndexDeParser(StringBuilder buffer)
        {
            this.buffer = buffer;
        }

        public void deParse(CreateIndex createIndex)
        {
            Index index = createIndex.getIndex();

            buffer.Append("CREATE ");

            if (index.getType() != null)
            {
                buffer.Append(index.getType());
                buffer.Append(" ");
            }

            buffer.Append("INDEX ");
            buffer.Append(index.getName());
            buffer.Append(" ON ");
            buffer.Append(createIndex.getTable().GetFullyQualifiedName());

            if (index.getColumnsNames() != null)
            {
                buffer.Append(" (");
                foreach (var columnName in index.getColumnsNames())
                {
                    buffer.Append(columnName);
                    if (!(columnName == index.getColumnsNames().Last()))
                    {
                        buffer.Append(", ");
                    }
                }

                buffer.Append(")");
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
