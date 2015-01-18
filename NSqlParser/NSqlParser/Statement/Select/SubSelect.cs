/**
 * A subselect followed by an optional alias.
 */

using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSqlParser.Expression;
using NSqlParser.Expression.Operators.Relational;

namespace NSqlParser.Statement.Select
{
    public class SubSelect : IFromItem, IExpression, IItemsList
    {

        private ISelectBody selectBody;
        private Alias alias;
        private bool useBrackets = true;
        private List<WithItem> withItemsList;

        private Pivot pivot;

        public void Accept(IFromItemVisitor fromItemVisitor)
        {
            fromItemVisitor.Visit(this);
        }

        public ISelectBody getSelectBody()
        {
            return selectBody;
        }

        public void setSelectBody(ISelectBody body)
        {
            selectBody = body;
        }

        public void Accept(IExpressionVisitor expressionVisitor)
        {
            expressionVisitor.Visit(this);
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

        public bool isUseBrackets()
        {
            return useBrackets;
        }

        public void setUseBrackets(bool useBrackets)
        {
            this.useBrackets = useBrackets;
        }

        public List<WithItem> getWithItemsList()
        {
            return withItemsList;
        }

        public void setWithItemsList(List<WithItem> withItemsList)
        {
            this.withItemsList = withItemsList;
        }

        public void Accept(IItemsListVisitor itemsListVisitor)
        {
            itemsListVisitor.Visit(this);
        }

        public override string ToString()
        {
            StringBuilder retval = new StringBuilder();
            if (useBrackets)
                retval.Append("(");
            if (withItemsList != null && withItemsList.Count > 0)
            {
                retval.Append("WITH ");
                foreach (var withItem in withItemsList)
                {
                    retval.Append(withItem);
                    if (!(withItem == withItemsList.Last()))
                        retval.Append(",");
                    retval.Append(" ");
                }
            }
            retval.Append(selectBody);
            if (useBrackets)
                retval.Append(")");

            if (pivot != null) retval.Append(" ").Append(pivot);
            if (alias != null) retval.Append(alias.ToString());

            return retval.ToString();
        }
    }
}
