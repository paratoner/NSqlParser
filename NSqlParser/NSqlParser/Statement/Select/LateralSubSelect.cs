/**
 * A lateral subselect followed by an alias.
 *
 * @author Paratoner
 */

using NSqlParser.Expression;

namespace NSqlParser.Statement.Select
{
    public class LateralSubSelect : IFromItem
    {

        private SubSelect subSelect;
        private Alias alias;
        private Pivot pivot;

        public void setSubSelect(SubSelect subSelect)
        {
            this.subSelect = subSelect;
        }

        public SubSelect getSubSelect()
        {
            return subSelect;
        }

        public void Accept(IFromItemVisitor fromItemVisitor)
        {
            fromItemVisitor.Visit(this);
        }

        public Alias getAlias()
        {
            return alias;
        }

        public void setAlias(Alias alias)
        {
            this.alias = alias;
        }

        public Pivot getPivot()
        {
            return pivot;
        }

        public void setPivot(Pivot pivot)
        {
            this.pivot = pivot;
        }

        public override string ToString()
        {
            return "LATERAL" + subSelect.ToString() +
                   ((pivot != null) ? " " + pivot : "") +
                   ((alias != null) ? alias.ToString() : "");
        }
    }
}
