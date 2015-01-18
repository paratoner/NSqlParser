/**
 * An item in a "SELECT [...] FROM item1" statement. (for example a table or a
 * sub-select)
 */

using NSqlParser.Expression;

namespace NSqlParser.Statement.Select
{
    public interface IFromItem {

        void Accept(IFromItemVisitor fromItemVisitor);

        Alias getAlias();

        void setAlias(Alias alias);

        Pivot getPivot();

        void setPivot(Pivot pivot);

    }
}
