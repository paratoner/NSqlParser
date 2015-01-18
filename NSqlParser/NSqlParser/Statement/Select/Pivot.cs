using System.Collections.Generic;
using NSqlParser.Schema;

namespace NSqlParser.Statement.Select
{
    public class Pivot
    {

        private List<FunctionItem> functionItems;

        private List<Column> forColumns;

        private List<SelectExpressionItem> singleInItems;

        private List<ExpressionListItem> multiInItems;

        public virtual void Accept(IPivotVisitor pivotVisitor)
        {
            pivotVisitor.Visit(this);
        }

        public List<SelectExpressionItem> getSingleInItems()
        {
            return singleInItems;
        }

        public void setSingleInItems(List<SelectExpressionItem> singleInItems)
        {
            this.singleInItems = singleInItems;
        }

        public List<ExpressionListItem> getMultiInItems()
        {
            return multiInItems;
        }

        public void setMultiInItems(List<ExpressionListItem> multiInItems)
        {
            this.multiInItems = multiInItems;
        }

        public List<FunctionItem> getFunctionItems()
        {
            return functionItems;
        }

        public void setFunctionItems(List<FunctionItem> functionItems)
        {
            this.functionItems = functionItems;
        }

        public List<Column> getForColumns()
        {
            return forColumns;
        }

        public void setForColumns(List<Column> forColumns)
        {
            this.forColumns = forColumns;
        }

        //public List<SelectExpressionItem> getSingleInItems()
        //{
        //     return singleInItems;
        //}
        //public List<ExpressionListItem> getMultiInItems()
        //{
        //        return multiInItems;
        //}

        public override string ToString()
        {
            return "PIVOT (" +
                   PlainSelect.getStringList(functionItems) +
                   " FOR " + PlainSelect.getStringList(forColumns, true, forColumns != null && forColumns.Count > 1) +
                   " IN " + PlainSelect.getStringList(getSingleInItems(), true, true) + ")";
        }
    }
}
