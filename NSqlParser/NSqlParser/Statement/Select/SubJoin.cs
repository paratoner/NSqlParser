/**
 * A table created by "(tab1 join tab2)".
 */

using NSqlParser.Expression;

namespace NSqlParser.Statement.Select
{
    public class SubJoin : IFromItem
    {

        private IFromItem left;
        private Join join;
        private Alias alias;
        private Pivot pivot;

        public void Accept(IFromItemVisitor fromItemVisitor)
        {
            fromItemVisitor.Visit(this);
        }

        public IFromItem getLeft()
        {
            return left;
        }

        public void setLeft(IFromItem l)
        {
            left = l;
        }

        public Join getJoin()
        {
            return join;
        }

        public void setJoin(Join j)
        {
            join = j;
        }

        public Pivot getPivot()
        {
            return pivot;
        }

        public void setPivot(Pivot pivot)
        {
            this.pivot = pivot;
        }

        public Alias getAlias()
        {
            return alias;
        }

        public void setAlias(Alias alias)
        {
            this.alias = alias;
        }

        public override string ToString()
        {
            return "(" + left + " " + join + ")"
                   + ((pivot != null) ? " " + pivot : "")
                   + ((alias != null) ? alias.ToString() : "");
        }
    }
}
