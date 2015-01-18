/**
 * A class to de-parse (that is, tranform from NSqlParser hierarchy into a
 * string) a {@link NSqlParser.statement.create.view.CreateView}
 */

using System.Text;
using NSqlParser.Statement.Create.View;
using NSqlParser.Statement.Select;

namespace NSqlParser.Util.Deparser
{
    public class CreateViewDeParser
    {

        private StringBuilder buffer;

        /**
     * @param buffer the buffer that will be filled with the select
     */
        public CreateViewDeParser(StringBuilder buffer)
        {
            this.buffer = buffer;
        }

        public void deParse(CreateView createView)
        {
            buffer.Append("CREATE ");
            if (createView.isOrReplace())
            {
                buffer.Append("OR REPLACE ");
            }
            if (createView.isMaterialized())
            {
                buffer.Append("MATERIALIZED ");
            }
            buffer.Append("VIEW ").Append(createView.getView().GetFullyQualifiedName());
            if (createView.getColumnNames() != null)
            {
                buffer.Append(PlainSelect.getStringList(createView.getColumnNames(), true, true));
            }
            buffer.Append(" AS ");
            buffer.Append(createView.getSelectBody().ToString());
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
