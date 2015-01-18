using System.Collections.Generic;
using NSqlParser.Schema;

namespace NSqlParser.Statement.Select
{
    public class PivotXml : Pivot
    {

        private ISelectBody inSelect;
        private bool inAny = false;

        public override void Accept(IPivotVisitor pivotVisitor)
        {
            pivotVisitor.Visit(this);
        }

        public ISelectBody getInSelect()
        {
            return inSelect;
        }

        public void setInSelect(ISelectBody inSelect)
        {
            this.inSelect = inSelect;
        }

        public bool isInAny()
        {
            return inAny;
        }

        public void setInAny(bool inAny)
        {
            this.inAny = inAny;
        }

        public override string ToString()
        {
            List<Column> forColumns = getForColumns();
            string inn = inAny ? "ANY" : inSelect == null ? PlainSelect.getStringList(getSingleInItems()) : inSelect.ToString();
            return "PIVOT XML (" +
                   PlainSelect.getStringList(getFunctionItems()) +
                   " FOR " + PlainSelect.getStringList(forColumns, true, forColumns != null && forColumns.Count > 1) +
                   " IN (" + inn + "))";
        }

    }
}
